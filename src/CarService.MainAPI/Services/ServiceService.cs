using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class ServiceService : BaseService<Service>, IServiceService
    {
        public ServiceService(IRepository<Service> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }
    }
}
