using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models.Services;

namespace CarService.UI.Services
{
    internal class ServiceService : IServiceService
    {
        private readonly IMainClient _mainClient;
        public ServiceService(IMainClient mainClient)
        {
            _mainClient = mainClient;
        }

        public async Task CreateService(string token, ServiceViewModel model)
        {
            await _mainClient.CreateService(token, model);
        }

        public async Task DeleteService(string token, int id)
        {
            await _mainClient.DeleteService(token, id);
        }

        public async Task EditService(string token, int id, ServiceViewModel model)
        {
            await _mainClient.EditService(token, id, model);
        }

        public async Task<IEnumerable<ServiceViewModel>> GetAllByServiceDataId(string token, int serviceDataId)
        {
            return await _mainClient.GetServiceViewModels(token, serviceDataId);
        }

        public async Task<ServiceViewModel> GetServiceById(string token, int id)
        {
            return await _mainClient.GetServiceById(token, id);
        }
    }
}
