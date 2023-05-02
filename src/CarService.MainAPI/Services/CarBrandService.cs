using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class CarBrandService : BaseService<CarBrand>, ICarBrandService 
    {
        public CarBrandService(IRepository<CarBrand> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }
    }
}
