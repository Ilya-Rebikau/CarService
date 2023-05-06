using CarService.UI.Models.Services;

namespace CarService.UI.Interfaces
{
    public interface IServiceDataService
    {
        Task<IEnumerable<ServiceDataViewModel>> GetServiceDatas(string token, int pageNumber);
        Task<ServiceDataViewModel> GetServiceDataById(string token, int id);
        Task CreateServiceData(string token, ServiceDataViewModel model);
        Task EditServiceData(string token, int id, ServiceDataViewModel model);
        Task DeleteServiceData(string token, int id);
    }
}
