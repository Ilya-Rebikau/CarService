using CarService.DAL.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface IServiceDataService : IBaseService<ServiceData>
    {
        IEnumerable<ServiceData> GetAll();
    }
}
