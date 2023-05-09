using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        private readonly IRepository<Service> _serviceRepository;
        public AppointmentService(IRepository<Appointment> repository, IConfiguration configuration,
            IRepository<Service> serviceRepository)
            : base(repository, configuration)
        {
            _serviceRepository = serviceRepository;
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

        public IEnumerable<Appointment> GetAllByUserId(string userId, int pageNumber)
        {
            var appointments = Repository.GetAll().Where(a => a.UserId == userId);
            appointments = appointments.OrderByDescending(a => a.DateTimeStart).Skip((pageNumber - 1) * CountOnPage).Take(CountOnPage);
            return appointments;
        }

        public override async Task<Appointment> Create(Appointment obj)
        {
            var service = await _serviceRepository.GetById(obj.ServiceId);
            obj.DateTimeEnd = obj.DateTimeStart.AddMinutes(service.MinutesSpent);
            await CheckForSameTimeAndServiceData(obj, service);
            CheckForRightTime(obj);
            return await base.Create(obj);
        }

        private static void CheckForRightTime(Appointment obj)
        {
            var maxTimeEnd = new TimeSpan(20, 0, 0);
            var minTimeStart = new TimeSpan(8, 0, 0);
            if (obj.DateTimeStart.Hour >= maxTimeEnd.Hours || obj.DateTimeStart.Hour < minTimeStart.Hours ||
                obj.DateTimeEnd.Hour > maxTimeEnd.Hours)
            {
                throw new MyException($"Мы не работаем в это время {obj.DateTimeStart.ToShortTimeString()} - {obj.DateTimeEnd.ToShortTimeString()}!");
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
    }
}
