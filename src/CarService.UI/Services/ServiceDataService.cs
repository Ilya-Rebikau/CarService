using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models.Services;

namespace CarService.UI.Services
{
    public class ServiceDataService : IServiceDataService
    {
        private readonly IMainClient _mainClient;
        public ServiceDataService(IMainClient mainClient)
        {
            _mainClient = mainClient;
        }

        public async Task CreateServiceData(string token, ServiceDataViewModel model)
        {
            if (model.Image is not null)
            {
                using var binaryReader = new BinaryReader(model.Image.OpenReadStream());
                byte[] imageData = binaryReader.ReadBytes((int)model.Image.Length);
                model.ImageData = imageData;
            }

            await _mainClient.CreateServiceData(token, model);
        }

        public async Task DeleteServiceData(string token, int id)
        {
            await _mainClient.DeleteServiceData(token, id);
        }

        public async Task EditServiceData(string token, int id, ServiceDataViewModel model)
        {
            if (model.DeletePhoto)
            {
                model.ImageData = null;
            }
            else if (model.Image is not null)
            {
                using var binaryReader = new BinaryReader(model.Image.OpenReadStream());
                byte[] imageData = binaryReader.ReadBytes((int)model.Image.Length);
                model.ImageData = imageData;
            }
            else
            {
                model.ImageData = model.ImageData;
            }

            await _mainClient.EditServiceData(token, id, model);
        }

        public async Task<ServiceDataViewModel> GetServiceDataById(string token, int id)
        {
            return await _mainClient.GetServiceDataById(token, id);
        }

        public async Task<IEnumerable<ServiceDataViewModel>> GetServiceDatas(string token, int pageNumber)
        {
            return await _mainClient.GetServiceDatas(token, pageNumber);
        }

        public async Task<IEnumerable<ServiceDataViewModel>> GetServiceDatas(string token)
        {
            return await _mainClient.GetServiceDatas(token);
        }
    }
}
