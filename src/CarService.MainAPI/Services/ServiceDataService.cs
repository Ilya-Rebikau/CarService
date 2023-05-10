using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class ServiceDataService : BaseService<ServiceData>, IServiceDataService
    {
        public ServiceDataService(IRepository<ServiceData> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }

        public IEnumerable<ServiceData> GetAll()
        {
            return Repository.GetAll();
        }

        public override async Task<ServiceData> Create(ServiceData obj)
        {
            CheckForSameName(obj);
            return await base.Create(obj);
        }

        public override async Task<ServiceData> Update(ServiceData obj)
        {
            CheckForSameName(obj);
            return await base.Update(obj);
        }

        private void CheckForSameName(ServiceData obj)
        {
            if (Repository.GetAll().Any(sd => sd.Name.ToLower() == obj.Name.ToLower() && sd.Id != obj.Id))
            {
                throw new MyException("Такая услуга уже есть!");
            }
        }
    }
}
