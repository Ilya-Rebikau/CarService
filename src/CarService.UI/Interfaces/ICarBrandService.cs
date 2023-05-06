using CarService.UI.Models.CarBrands;

namespace CarService.UI.Interfaces
{
    public interface ICarBrandService
    {
        Task<IEnumerable<CarBrandViewModel>> GetCarBrandViewModels(string token, int pageNumber);
        Task<IEnumerable<CarBrandViewModel>> GetCarBrandViewModels(string token);
        Task CreateCarBrand(string token, CarBrandViewModel model);
        Task<CarBrandViewModel> GetCarBrandViewModelForEdit(string token, int id);
        Task EditCarBrand(string token, int id, CarBrandViewModel model);
        Task DeleteCarBrand(string token, int id);
    }
}
