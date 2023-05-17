using CarService.DAL.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface ICarTypeService : IBaseService<CarType>
    {
        IEnumerable<CarType> GetAll();
    }
}
