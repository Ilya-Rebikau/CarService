﻿using AutoMapper;
using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Services
{
    internal class ServiceService : BaseService<Service>, IServiceService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CarType> _carTypeRepository;
        private readonly IRepository<CarBrand> _carBrandRepository;
        private readonly IRepository<Discount> _discountRepository;
        public ServiceService(IRepository<Service> repository, IConfiguration configuration,
            IMapper mapper, IRepository<CarType> carTypeRepository, IRepository<CarBrand> carBrandRepository,
            IRepository<Discount> discountRepository)
            : base(repository, configuration)
        {
            _mapper = mapper;
            _carTypeRepository = carTypeRepository;
            _carBrandRepository = carBrandRepository;
            _discountRepository = discountRepository;
        }

        public async Task<ServiceModel> GetServiceModelById(int id)
        {
            var service = await Repository.GetById(id);
            var serviceModel = _mapper.Map<ServiceModel>(service);
            var discountPercent = GetMaxDiscount(service.CarTypeId, service.CarBrandId, service.ServiceDataId);
            if (discountPercent > 0)
            {
                serviceModel.NewPrice = service.Price * ((100 - discountPercent) / 100);
            }

            if (service.CarBrandId is not null)
            {
                var carBrand = await _carBrandRepository.GetById((int)service.CarBrandId);
                serviceModel.CarBrandName = carBrand.Name;
            }
            else
            {
                serviceModel.CarBrandName = "Любой";
            }

            if (service.CarTypeId is not null)
            {
                var carType = await _carTypeRepository.GetById((int)service.CarTypeId);
                serviceModel.CarTypeName = carType.Name;
            }
            else
            {
                serviceModel.CarTypeName = "Любая";
            }

            return serviceModel;
        }

        public async Task<IEnumerable<ServiceModel>> GetAllByServiceDataId(int serviceDataId)
        {
            var services = Repository.GetAll().Where(s => s.ServiceDataId == serviceDataId).ToList();
            var serviceModels = new List<ServiceModel>();
            foreach (var service in services)
            {
                var serviceModel = _mapper.Map<ServiceModel>(service);
                if (service.CarBrandId is not null)
                {
                    var carBrand = await _carBrandRepository.GetById((int)service.CarBrandId);
                    serviceModel.CarBrandName = carBrand.Name;
                }
                else
                {
                    serviceModel.CarBrandName = "Любой";
                }

                if (service.CarTypeId is not null)
                {
                    var carType = await _carTypeRepository.GetById((int)service.CarTypeId);
                    serviceModel.CarTypeName = carType.Name;
                }
                else
                {
                    serviceModel.CarTypeName = "Любая";
                }
                
                var discountPercent = GetMaxDiscount(service.CarTypeId, service.CarBrandId, service.ServiceDataId);
                if (discountPercent > 0)
                {
                    serviceModel.NewPrice = service.Price * ((100 - discountPercent) / 100m);
                }

                serviceModels.Add(serviceModel);
            }

            serviceModels = serviceModels.OrderBy(s => s.CarTypeId).ThenBy(s => s.CarBrandId).ToList();
            return serviceModels;
        }

        public async Task CreateServiceModel(ServiceModel serviceModel)
        {
            var service = _mapper.Map<Service>(serviceModel);
            await Create(service);
        }

        public async Task UpdateServiceModel(ServiceModel serviceModel)
        {
            var service = _mapper.Map<Service>(serviceModel);
            await Update(service);
        }

        public override async Task<Service> Create(Service obj)
        {
            CheckForSameService(obj);
            return await base.Create(obj);
        }

        public override async Task<Service> Update(Service obj)
        {
            CheckForSameService(obj);
            return await base.Update(obj);
        }

        private void CheckForSameService(Service service)
        {
            if (Repository.GetAll().Any(s => s.CarBrandId == null && s.CarTypeId == null &&
                s.ServiceDataId == service.ServiceDataId && s.Id != service.Id))
            {
                throw new MyException("Уже есть услуга для любых видов авто");
            }

            if (Repository.GetAll().Any(s => s.CarTypeId == service.CarTypeId && s.CarBrandId == service.CarBrandId
                && s.ServiceDataId == service.ServiceDataId && s.Id != service.Id))
            {
                throw new MyException("Такой вид услуги для такой техники и марки уже есть!");
            }
        }

        private int GetMaxDiscount(int? carTypeId, int? carBrandId, int serviceDataId)
        {
            var currentDiscounts = _discountRepository.GetAll().Where(d => d.DateStart <= DateTime.Now &&
                d.DateEnd >= DateTime.Now).ToList();
            if (currentDiscounts.Any())
            {
                var percents = new List<int>
                {
                    GetDiscountPercentForAll(currentDiscounts),
                    GetDiscountPercentForType(currentDiscounts, carTypeId),
                    GetDiscountPercentForBrand(currentDiscounts, carBrandId),
                    GetDiscountPercentForServiceData(currentDiscounts, serviceDataId),
                    GetDiscountPercentForTypeAndBrand(currentDiscounts, carTypeId, carBrandId),
                    GetDiscountPercentForTypeAndService(currentDiscounts, carTypeId, serviceDataId),
                    GetDiscountPercentForBrandAndService(currentDiscounts, carBrandId, serviceDataId),
                    GetDiscountPercentForBrandAndTypeAndService(currentDiscounts, carBrandId, carTypeId, serviceDataId)
                };
                return percents.Max();
            }

            return 0;
        }

        private static int GetDiscountPercentForAll(List<Discount> discounts)
        {
            discounts = discounts.Where(d => d.CarTypeId == null &&
                d.CarBrandId == null && d.ServiceDataId == null).ToList();
            int percent = 0;
            if (discounts is not null && discounts.Any())
            {
                percent = discounts.Max(d => d.Percent);
            }

            return percent;
        }

        private static int GetDiscountPercentForType(List<Discount> discounts, int? carTypeId)
        {
            discounts = discounts.Where(d => d.CarTypeId == carTypeId &&
                d.CarBrandId == null && d.ServiceDataId == null).ToList();
            int percent = 0;
            if (discounts is not null && discounts.Any())
            {
                percent = discounts.Max(d => d.Percent);
            }

            return percent;
        }

        private static int GetDiscountPercentForBrand(List<Discount> discounts, int? carBrandId)
        {
            discounts = discounts.Where(d => d.CarTypeId == null &&
                d.CarBrandId == carBrandId && d.ServiceDataId == null).ToList();
            int percent = 0;
            if (discounts is not null && discounts.Any())
            {
                percent = discounts.Max(d => d.Percent);
            }

            return percent;
        }

        private static int GetDiscountPercentForServiceData(List<Discount> discounts, int serviceDataId)
        {
            discounts = discounts.Where(d => d.CarTypeId == null &&
                d.CarBrandId == null && d.ServiceDataId == serviceDataId).ToList();
            int percent = 0;
            if (discounts is not null && discounts.Any())
            {
                percent = discounts.Max(d => d.Percent);
            }

            return percent;
        }

        private static int GetDiscountPercentForTypeAndBrand(List<Discount> discounts, int? carTypeId, int? carBrandId)
        {
            discounts = discounts.Where(d => d.CarTypeId == carTypeId &&
                d.CarBrandId == carBrandId && d.ServiceDataId == null).ToList();
            int percent = 0;
            if (discounts is not null && discounts.Any())
            {
                percent = discounts.Max(d => d.Percent);
            }

            return percent;
        }

        private static int GetDiscountPercentForTypeAndService(List<Discount> discounts, int? carTypeId, int serviceDataId)
        {
            discounts = discounts.Where(d => d.CarTypeId == carTypeId &&
                d.CarBrandId == null && d.ServiceDataId == serviceDataId).ToList();
            int percent = 0;
            if (discounts is not null && discounts.Any())
            {
                percent = discounts.Max(d => d.Percent);
            }

            return percent;
        }

        private static int GetDiscountPercentForBrandAndService(List<Discount> discounts, int? carBrandId, int serviceDataId)
        {
            discounts = discounts.Where(d => d.CarTypeId == null &&
                d.CarBrandId == carBrandId && d.ServiceDataId == serviceDataId).ToList();
            int percent = 0;
            if (discounts is not null && discounts.Any())
            {
                percent = discounts.Max(d => d.Percent);
            }

            return percent;
        }

        private static int GetDiscountPercentForBrandAndTypeAndService(List<Discount> discounts, int? carBrandId,
            int? carTypeId, int serviceDataId)
        {
            discounts = discounts.Where(d => d.CarTypeId == carTypeId &&
                d.CarBrandId == carBrandId && d.ServiceDataId == serviceDataId).ToList();
            int percent = 0;
            if (discounts is not null && discounts.Any())
            {
                percent = discounts.Max(d => d.Percent);
            }

            return percent;
        }
    }
}
