using CarService.UI.Models;

namespace CarService.UI.Interfaces
{
    public interface ICarTypeService
    {
        Task<IEnumerable<CarType>> GetAll(string token);
    }
}
