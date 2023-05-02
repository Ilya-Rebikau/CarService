using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class CarTypeService : BaseService<CarType>, ICarTypeService
    {
        public CarTypeService(IRepository<CarType> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }
    }
}
