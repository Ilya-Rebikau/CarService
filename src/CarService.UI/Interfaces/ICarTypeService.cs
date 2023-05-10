using CarService.UI.Models;

namespace CarService.UI.Interfaces
{
    public interface ICarTypeService
    {
        Task<IEnumerable<CarTypeViewModel>> GetAll(string token);
    }
}
