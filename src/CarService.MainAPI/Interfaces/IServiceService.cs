using CarService.DAL.Models;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface IServiceService : IBaseService<Service>
    {
        ServiceListModel GetServiceListModel(int carTypeId, int carBrandId);
        Task<ServiceModel> GetServiceModelById(int id);
        ServiceModel GetServiceModelForCreate();
    }
}
