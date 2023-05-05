using CarService.UI.Models.Services;

namespace CarService.UI.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceListViewModel> GetServiceListViewModel(string token, int carTypeId, int carBrandId);
        Task<ServiceViewModel> GetServiceViewModelById(string token, int id);
        Task<ServiceViewModel> GetServiceViewModelForCreate(string token);
        Task CreateService(string token, ServiceViewModel model);
        Task EditService(string token, int id, ServiceViewModel model);
        Task DeleteService(string token, int id);
    }
}
