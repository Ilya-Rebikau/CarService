using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;
using CarService.UI.Models.CarBrands;
using CarService.UI.Models.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<ServiceListViewModel> GetServiceListViewModel(string token, int carTypeId, int carBrandId)
        {
            var serviceListViewModel = await _mainClient.GetServiceListViewModel(token, carTypeId, carBrandId);
            CarType tempCarType;
            CarBrand tempCarBrand;
            serviceListViewModel.CarTypeSelectList = new SelectList(serviceListViewModel.CarTypes,
                nameof(tempCarType.Id), nameof(tempCarType.Name));
            serviceListViewModel.CarBrandSelectList = new SelectList(serviceListViewModel.CarBrands,
                nameof(tempCarBrand.Id), nameof(tempCarBrand.Name));
            return serviceListViewModel;
        }

        public async Task<ServiceViewModel> GetServiceViewModelById(string token, int id)
        {
            var serviceViewModel = await _mainClient.ServiceDetails(token, id);
            CarType tempCarType;
            CarBrand tempCarBrand;
            serviceViewModel.CarTypeSelectList = new SelectList(serviceViewModel.CarTypes,
                nameof(tempCarType.Id), nameof(tempCarType.Name));
            serviceViewModel.CarBrandSelectList = new SelectList(serviceViewModel.CarBrands,
                nameof(tempCarBrand.Id), nameof(tempCarBrand.Name));
            return serviceViewModel;
        }

        public async Task<ServiceViewModel> GetServiceViewModelForCreate(string token)
        {
            var serviceViewModel = await _mainClient.GetServiceViewModelForCreate(token);
            CarType tempCarType;
            CarBrand tempCarBrand;
            serviceViewModel.CarTypeSelectList = new SelectList(serviceViewModel.CarTypes,
                nameof(tempCarType.Id), nameof(tempCarType.Name));
            serviceViewModel.CarBrandSelectList = new SelectList(serviceViewModel.CarBrands,
                nameof(tempCarBrand.Id), nameof(tempCarBrand.Name));
            return serviceViewModel;
        }
    }
}
