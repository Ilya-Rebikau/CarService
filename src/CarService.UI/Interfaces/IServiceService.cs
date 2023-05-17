using CarService.UI.Models.Service;

namespace CarService.UI.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceViewModel>> GetAllByServiceDataId(string token, int serviceDataId);
        Task CreateService(string token, ServiceViewModel model);
        Task EditService(string token, int id, ServiceViewModel model);
        Task DeleteService(string token, int id);
        Task<ServiceViewModel> GetServiceById(string token, int id);
    }
}
