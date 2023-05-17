using CarService.UI.Models;
using RestEase;

namespace CarService.UI.Interfaces
{
    public interface ICarTypeService
    {
        Task<IEnumerable<CarTypeViewModel>> GetAll(string token);
        Task CreateCarType(string token, CarTypeViewModel model);
        Task<CarTypeViewModel> GetCarTypeViewModelForEdit(string token, [Path] int id);
        Task EditCarType(string token, [Path] int id, [Body] CarTypeViewModel model);
        Task DeleteCarType(string token, [Path] int id);
    }
}
