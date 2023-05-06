using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;

namespace CarService.UI.Services
{
    public class CarBrandService : ICarBrandService
    {
        private readonly IMainClient _mainClient;
        public CarBrandService(IMainClient mainClient)
        {
            _mainClient = mainClient;
        }

        public async Task CreateCarBrand(string token, CarBrandViewModel model)
        {
            if (model.Image is not null)
            {
                using var binaryReader = new BinaryReader(model.Image.OpenReadStream());
                byte[] imageData = binaryReader.ReadBytes((int)model.Image.Length);
                model.ImageData = imageData;
            }

            await _mainClient.CreateCarBrand(token, model);
        }

        public async Task DeleteCarBrand(string token, int id)
        {
            await _mainClient.DeleteCarBrand(token, id);
        }

        public async Task EditCarBrand(string token, int id, CarBrandViewModel model)
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

            await _mainClient.EditCarBrand(token, id, model);
        }

        public async Task<CarBrandViewModel> GetCarBrandViewModelForEdit(string token, int id)
        {
            return await _mainClient.GetCarBrandViewModelForEdit(token, id);
        }

        public async Task<IEnumerable<CarBrandViewModel>> GetCarBrandViewModels(string token, int pageNumber)
        {
            return await _mainClient.GetCarBrandViewModels(token, pageNumber);
        }

        public async Task<IEnumerable<CarBrandViewModel>> GetCarBrandViewModels(string token)
        {
            return await _mainClient.GetCarBrandViewModels(token);
        }
    }
}
