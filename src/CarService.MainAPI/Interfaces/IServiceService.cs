using CarService.DAL.Models;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface IServiceService : IBaseService<Service>
    {
        Task<IEnumerable<ServiceModel>> GetAllByServiceDataId(int serviceDataId);
        Task<ServiceModel> GetServiceModelById(int id);
        Task CreateServiceModel(ServiceModel serviceModel);
        Task UpdateServiceModel(ServiceModel serviceModel);
    }
}
