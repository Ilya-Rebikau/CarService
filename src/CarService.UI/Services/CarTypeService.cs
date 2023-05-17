using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;
using RestEase;

namespace CarService.UI.Services
{
    internal class CarTypeService : ICarTypeService
    {
        private readonly IMainClient _mainClient;
        public CarTypeService(IMainClient mainClient)
        {
            _mainClient = mainClient;
        }

        public async Task CreateCarType(string token, CarTypeViewModel model)
        {
            await _mainClient.CreateCarType(token, model);
        }

        public async Task DeleteCarType(string token, [Path] int id)
        {
            await _mainClient.DeleteCarType(token, id);
        }

        public async Task EditCarType(string token, [Path] int id, [Body] CarTypeViewModel model)
        {
            await _mainClient.EditCarType(token, id, model);
        }

        public async Task<IEnumerable<CarTypeViewModel>> GetAll(string token)
        {
            return await _mainClient.GetCarTypes(token);
        }

        public async Task<CarTypeViewModel> GetCarTypeViewModelForEdit(string token, [Path] int id)
        {
            return await _mainClient.GetCarTypeViewModelForEdit(token, id);
        }
    }
}
