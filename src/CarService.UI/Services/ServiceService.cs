using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models.Service;

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
            if (model.Image is not null)
            {
                using var binaryReader = new BinaryReader(model.Image.OpenReadStream());
                byte[] imageData = binaryReader.ReadBytes((int)model.Image.Length);
                model.ImageData = imageData;
            }

            await _mainClient.CreateService(token, model);
        }

        public async Task DeleteService(string token, int id)
        {
            await _mainClient.DeleteService(token, id);
        }

        public async Task EditService(string token, int id, ServiceViewModel model)
        {
            if (model.Image is not null)
            {
                using var binaryReader = new BinaryReader(model.Image.OpenReadStream());
                byte[] imageData = binaryReader.ReadBytes((int)model.Image.Length);
                model.ImageData = imageData;
            }
            else
            {
                model.ImageData = null;
            }

            await _mainClient.EditService(token, id, model);
        }

        public async Task<ServiceViewModel> GetServiceViewModelForDelete(string token, int id)
        {
            return await _mainClient.GetServiceViewModelForDelete(token, id);
        }

        public async Task<ServiceViewModel> GetServiceViewModelForEdit(string token, int id)
        {
            return await _mainClient.GetServiceViewModelForEdit(token, id);
        }

        public async Task<IEnumerable<ServiceViewModel>> GetServiceViewModels(string token, int pageNumber)
        {
            return await _mainClient.GetServiceViewModels(token, pageNumber);
        }

        public async Task<ServiceViewModel> ServiceDetails(string token, int id)
        {
            return await _mainClient.ServiceDetails(token, id);
        }
    }
}
