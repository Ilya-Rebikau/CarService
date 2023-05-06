using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class ServiceDataService : BaseService<ServiceData>, IServiceDataService
    {
        public ServiceDataService(IRepository<ServiceData> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }
    }
}
