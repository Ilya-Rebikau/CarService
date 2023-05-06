using AutoMapper;
using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Services
{
    internal class ServiceService : BaseService<Service>, IServiceService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CarType> _carTypeRepository;
        private readonly IRepository<CarBrand> _carBrandRepository;
        public ServiceService(IRepository<Service> repository, IConfiguration configuration,
            IMapper mapper, IRepository<CarType> carTypeRepository, IRepository<CarBrand> carBrandRepository)
            : base(repository, configuration)
        {
            _mapper = mapper;
            _carTypeRepository = carTypeRepository;
            _carBrandRepository = carBrandRepository;
        }

        public async Task<ServiceModel> GetServiceModelById(int id)
        {
            var service = await Repository.GetById(id);
            var serviceModel = _mapper.Map<ServiceModel>(service);
            var carBrand = await _carBrandRepository.GetById(service.CarBrandId);
            var carType = await _carTypeRepository.GetById(service.CarTypeId);
            serviceModel.CarBrandName = carBrand.Name;
            serviceModel.CarTypeName = carType.Name;
            return serviceModel;
        }

        public async Task<IEnumerable<ServiceModel>> GetAllByServiceDataId(int serviceDataId)
        {
            var services = Repository.GetAll().Where(s => s.ServiceDataId == serviceDataId).ToList();
            var serviceModels = new List<ServiceModel>();
            foreach (var service in services)
            {
                var carBrand = await _carBrandRepository.GetById(service.CarBrandId);
                var carType = await _carTypeRepository.GetById(service.CarTypeId);
                var serviceModel = _mapper.Map<ServiceModel>(service);
                serviceModel.CarBrandName = carBrand.Name;
                serviceModel.CarTypeName = carType.Name;
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
    }
}
