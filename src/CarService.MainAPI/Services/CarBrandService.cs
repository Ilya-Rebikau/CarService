using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class CarBrandService : BaseService<CarBrand>, ICarBrandService 
    {
        public CarBrandService(IRepository<CarBrand> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }

        public override async Task<CarBrand> Create(CarBrand obj)
        {
            CheckForSameBrand(obj);
            return await base.Create(obj);
        }

        public override async Task<CarBrand> Update(CarBrand obj)
        {
            CheckForSameBrand(obj);
            return await base.Update(obj);
        }

        private void CheckForSameBrand(CarBrand obj)
        {
            if (Repository.GetAll().Any(cb => cb.Name == obj.Name && cb.Id != obj.Id))
            {
                throw new MyException("Такая марка автомобиля уже есть!");
            }
        }
    }
}
