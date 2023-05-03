using CarService.UI.Models.Service;

namespace CarService.UI.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceViewModel>> GetServiceViewModels(string token, int pageNumber);
        Task<ServiceViewModel> ServiceDetails(string token, int id);
        Task CreateService(string token, ServiceViewModel model);
        Task<ServiceViewModel> GetServiceViewModelForEdit(string token, int id);
        Task EditService(string token, int id, ServiceViewModel model);
        Task<ServiceViewModel> GetServiceViewModelForDelete(string token, int id);
        Task DeleteService(string token, int id);
    }
}
