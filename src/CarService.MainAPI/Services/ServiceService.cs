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
        private readonly IRepository<CarBrand> _carBrandRepository;
        private readonly IRepository<CarType> _carTypeRepository;
        public ServiceService(IRepository<Service> repository, IConfiguration configuration,
            IMapper mapper, IRepository<CarBrand> carBrandRepository, IRepository<CarType> carTypeRepository)
            : base(repository, configuration)
        {
            _mapper = mapper;
            _carBrandRepository = carBrandRepository;
            _carTypeRepository = carTypeRepository;
        }

        public ServiceListModel GetServiceListModel(int carTypeId, int carBrandId)
        {
            var services = Repository.GetAll()
                .Where(s => s.CarTypeId == carTypeId && s.CarBrandId == carBrandId).ToList();
            var serviceListModel = new ServiceListModel
            {
                Services = services,
                CarBrands = _carBrandRepository.GetAll(),
                CarTypes = _carTypeRepository.GetAll()
            };

            return serviceListModel;
        }

        public async Task<ServiceModel> GetServiceModelById(int id)
        {
            var service = await Repository.GetById(id);
            var serviceModel = _mapper.Map<ServiceModel>(service);
            serviceModel.CarBrands = _carBrandRepository.GetAll();
            serviceModel.CarTypes = _carTypeRepository.GetAll();
            return serviceModel;
        }

        public ServiceModel GetServiceModelForCreate()
        {
            var serviceModel = new ServiceModel
            {
                CarBrands = _carBrandRepository.GetAll(),
                CarTypes = _carTypeRepository.GetAll()
            };
            return serviceModel;
        }
    }
}
