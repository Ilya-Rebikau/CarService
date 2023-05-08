using AutoMapper;
using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Services
{
    internal class DiscountService : BaseService<Discount>, IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CarBrand> _carBrandRepository;
        private readonly IRepository<CarType> _carTypeRepository;
        private readonly IRepository<ServiceData> _serviceDataRepository;
        public DiscountService(IRepository<Discount> repository, IConfiguration configuration,
            IMapper mapper, IRepository<CarBrand> carBrandRepository, IRepository<CarType> carTypeRepository,
            IRepository<ServiceData> serviceDataRepository)
            : base(repository, configuration)
        {
            _mapper = mapper;
            _carBrandRepository = carBrandRepository;
            _carTypeRepository = carTypeRepository;
            _serviceDataRepository = serviceDataRepository;
        }

        public async Task CreateDiscountModel(DiscountModel model)
        {
            var discount = _mapper.Map<Discount>(model);
            await Create(discount);
        }

        public async Task<IEnumerable<DiscountModel>> GetAllDiscountModels()
        {
            var discounts = Repository.GetAll();
            var discountModels = new List<DiscountModel>();
            foreach (var discount in discounts)
            {
                var discountModel = _mapper.Map<DiscountModel>(discount);
                if (discount.CarBrandId is not null)
                {
                    var carBrand = await _carBrandRepository.GetById((int)discount.CarBrandId);
                    discountModel.CarBrandName = carBrand.Name;
                }
                else
                {
                    discountModel.CarBrandName = "Любая";
                }

                if (discount.CarTypeId is not null)
                {
                    var carType = await _carTypeRepository.GetById((int)discount.CarTypeId);
                    discountModel.CarTypeName = carType.Name;
                }
                else
                {
                    discountModel.CarTypeName = "Любой";
                }

                if (discount.ServiceDataId is not null)
                {
                    var serviceData = await _serviceDataRepository.GetById((int)discount.ServiceDataId);
                    discountModel.ServiceDataName = serviceData.Name;
                }
                else
                {
                    discountModel.ServiceDataName = "Любой";
                }

                discountModels.Add(discountModel);
            }

            return discountModels;
        }

        public async Task<DiscountModel> GetDiscountModelById(int id)
        {
            var discount = await Repository.GetById(id);
            var discountModel = _mapper.Map<DiscountModel>(discount);
            if (discount.CarBrandId is not null)
            {
                var carBrand = await _carBrandRepository.GetById((int)discount.CarBrandId);
                discountModel.CarBrandName = carBrand.Name;
            }
            else
            {
                discountModel.CarBrandName = "Любая";
            }

            if (discount.CarTypeId is not null)
            {
                var carType = await _carTypeRepository.GetById((int)discount.CarTypeId);
                discountModel.CarTypeName = carType.Name;
            }
            else
            {
                discountModel.CarTypeName = "Любой";
            }

            if (discount.ServiceDataId is not null)
            {
                var serviceData = await _serviceDataRepository.GetById((int)discount.ServiceDataId);
                discountModel.ServiceDataName = serviceData.Name;
            }
            else
            {
                discountModel.ServiceDataName = "Любой";
            }

            return discountModel;
        }

        public async Task UpdateDiscountModel(DiscountModel model)
        {
            var discount = _mapper.Map<Discount>(model);
            await Update(discount);
        }
    }
}
