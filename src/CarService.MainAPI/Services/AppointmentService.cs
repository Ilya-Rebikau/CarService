using AutoMapper;
using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;
using CarService.MainAPI.Interfaces.HttpClients;
using CarService.MainAPI.Models;
using Newtonsoft.Json.Linq;

namespace CarService.MainAPI.Services
{
    internal class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        private readonly int _promocodeDays;
        private readonly int _percent;
        private readonly int _appointmentCountForPromocode;
        private readonly int _maxAppointmentDate;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IUserClient _userClient;
        private readonly IRepository<CarType> _carTypeRepository;
        private readonly IRepository<CarBrand> _carBrandRepository;
        private readonly IRepository<ServiceData> _serviceDataRepository;
        private readonly IPromocodeService _promocodeService;
        public AppointmentService(IRepository<Appointment> repository, IConfiguration configuration,
            IRepository<Service> serviceRepository, IMapper mapper, IUserClient userClient,
            IRepository<ServiceData> serviceDataRepository, IRepository<CarType> carTypeRepository,
            IRepository<CarBrand> carBrandRepository, IPromocodeService promocodeService)
            : base(repository, configuration)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _userClient = userClient;
            _serviceDataRepository = serviceDataRepository;
            _carBrandRepository = carBrandRepository;
            _carTypeRepository = carTypeRepository;
            _promocodeService = promocodeService;
            _promocodeDays = configuration.GetValue<int>("PromocodeDays");
            _percent = configuration.GetValue<int>("Percent");
            _appointmentCountForPromocode = configuration.GetValue<int>("AppointmentCountForPromocode");
            _maxAppointmentDate = configuration.GetValue<int>("MaxAppointmentDate");
        }

        public async Task<IEnumerable<Appointment>> GetAllByDateAndServiceData(DateTime date, int serviceId)
        {
            var currentService = await _serviceRepository.GetById(serviceId);
            var appointmentsByDate = Repository.GetAll().Where(a => a.DateTimeStart.Date == date.Date);
            var appointments = new List<Appointment>();
            foreach (var appointment in appointmentsByDate)
            {
                var service = await _serviceRepository.GetById(appointment.ServiceId);
                if (service.ServiceDataId == currentService.ServiceDataId)
                {
                    appointments.Add(appointment);
                }
            }

            appointments = appointments.OrderBy(a => a.DateTimeStart).ToList();
            return appointments;
        }

        public async Task<IEnumerable<AppointmentModel>> GetAllByUserId(string userId)
        {
            var appointments = Repository.GetAll().Where(a => a.UserId == userId).OrderByDescending(a => a.DateTimeStart).ToList();
            if (appointments.Count(a => a.WasFinished) >= _appointmentCountForPromocode)
            {
                await GivePromocode(userId);
                foreach (var appointment in appointments.Where(a => a.WasFinished))
                {
                    await DeleteById(appointment.Id);
                }

                appointments = Repository.GetAll().Where(a => a.UserId == userId).OrderByDescending(a => a.DateTimeStart).ToList();
            }

            var appointmentModels = new List<AppointmentModel>();
            foreach (var appointment in appointments)
            {
                var appointmentModel = _mapper.Map<AppointmentModel>(appointment);
                var service = await _serviceRepository.GetById(appointment.ServiceId);
                var serviceData = await _serviceDataRepository.GetById(service.ServiceDataId);
                var carBrandName = string.Empty;
                var carTypeName = string.Empty;
                if (service.CarBrandId is not null)
                {
                    var carBrand = await _carBrandRepository.GetById((int)service.CarBrandId);
                    carBrandName = carBrand.Name;
                }
                else
                {
                    carBrandName = "Любой";
                }

                if (service.CarTypeId is not null)
                {
                    var carType = await _carTypeRepository.GetById((int)service.CarTypeId);
                    carTypeName = carType.Name;
                }
                else
                {
                    carTypeName = "Любая";
                }

                appointmentModel.ServiceData = $"{serviceData.Name}, марка {carBrandName}, тип {carTypeName}";
                appointmentModels.Add(appointmentModel);
            }

            return appointmentModels;
        }

        private async Task GivePromocode(string userId)
        {
            await _promocodeService.Create(new Promocode
            {
                UserId = userId,
                Percent = _percent,
                DateEnd = DateTime.Now.AddDays(_promocodeDays)
            });
        }

        public override async Task<Appointment> Create(Appointment obj)
        {
            var service = await _serviceRepository.GetById(obj.ServiceId);
            obj.DateTimeEnd = obj.DateTimeStart.AddMinutes(service.MinutesSpent);
            await CheckForSameTimeAndServiceData(obj, service);
            CheckForRightTime(obj);
            return await base.Create(obj);
        }

        private void CheckForRightTime(Appointment obj)
        {
            var maxTimeEnd = new TimeSpan(20, 0, 0);
            var minTimeStart = new TimeSpan(8, 0, 0);
            if (obj.DateTimeStart.Hour >= maxTimeEnd.Hours || obj.DateTimeStart.Hour < minTimeStart.Hours ||
                obj.DateTimeEnd.Hour > maxTimeEnd.Hours)
            {
                throw new MyException($"Мы не работаем в это время {obj.DateTimeStart.ToShortTimeString()} - {obj.DateTimeEnd.ToShortTimeString()}! " +
                    $"Наше время работы {minTimeStart} - {maxTimeEnd}");
            }

            if (obj.DateTimeStart.Date < DateTime.Now.Date)
            {
                throw new MyException("Нельзя записаться на прошедшую дату!");
            }

            if (obj.DateTimeStart.Date > DateTime.Now.Date.AddDays(_maxAppointmentDate))
            {
                throw new MyException($"Нельзя записаться дальше чем на {_maxAppointmentDate} дней");
            }
        }

        private async Task CheckForSameTimeAndServiceData(Appointment currentAppointment, Service currentService)
        {
            var appointments = Repository.GetAll().Where(a => a.Id != currentAppointment.Id).ToList();
            foreach (var appointment in appointments)
            {
                var service = await _serviceRepository.GetById(appointment.ServiceId);
                if (service.ServiceDataId == currentService.ServiceDataId &&
                    (IsBewteenTwoDates(currentAppointment.DateTimeStart, appointment.DateTimeStart, appointment.DateTimeEnd) ||
                    IsBewteenTwoDates(currentAppointment.DateTimeEnd, appointment.DateTimeStart, appointment.DateTimeEnd)))
                {
                    throw new MyException("Время уже занято!");
                }
            }
        }

        private static bool IsBewteenTwoDates(DateTime currentDateTime, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            return currentDateTime >= dateTimeStart && currentDateTime <= dateTimeEnd;
        }

        public async Task<IEnumerable<AppointmentModel>> GetAllAppointments(string token, int pageNumber)
        {
            var appointments = Repository.GetAll().OrderByDescending(a => a.DateTimeStart)
                .Skip((pageNumber - 1) * CountOnPage).Take(CountOnPage);
            var appointmentModels = new List<AppointmentModel>();
            foreach (var appointment in appointments)
            {
                var appointmentModel = _mapper.Map<AppointmentModel>(appointment);
                var service = await _serviceRepository.GetById(appointment.ServiceId);
                var serviceData = await _serviceDataRepository.GetById(service.ServiceDataId);
                var carBrandName = string.Empty;
                var carTypeName = string.Empty;
                if (service.CarBrandId is not null)
                {
                    var carBrand = await _carBrandRepository.GetById((int)service.CarBrandId);
                    carBrandName = carBrand.Name;
                }
                else
                {
                    carBrandName = "Любой";
                }

                if (service.CarTypeId is not null)
                {
                    var carType = await _carTypeRepository.GetById((int)service.CarTypeId);
                    carTypeName = carType.Name;
                }
                else
                {
                    carTypeName = "Любая";
                }

                appointmentModel.UserEmail = await _userClient.GetUserEmail(token, appointment.UserId);
                appointmentModel.ServiceData = $"{serviceData.Name}, марка {carBrandName}, тип {carTypeName}";
                appointmentModels.Add(appointmentModel);
            }

            return appointmentModels;
        }

        public async Task FinishAppointment(int id)
        {
            var appointment = await Repository.GetById(id);
            appointment.WasFinished = true;
            await Repository.Update(appointment);
        }
    }
}
