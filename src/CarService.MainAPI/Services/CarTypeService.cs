using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class CarTypeService : BaseService<CarType>, ICarTypeService
    {
        public CarTypeService(IRepository<CarType> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }

        public IEnumerable<CarType> GetAll()
        {
            return Repository.GetAll();
        }

        public override async Task<CarType> Create(CarType obj)
        {
            CheckForSameName(obj);
            return await base.Create(obj);
        }

        public override async Task<CarType> Update(CarType obj)
        {
            CheckForSameName(obj);
            return await base.Update(obj);
        }

        private void CheckForSameName(CarType obj)
        {
            if (Repository.GetAll().Any(ct => ct.Name == obj.Name && ct.Id != obj.Id))
            {
                throw new MyException("Такой тип техники уже есть!");
            }
        }
    }
}
