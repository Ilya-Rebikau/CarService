using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class ServiceAppointmentService : BaseService<ServiceAppointment>, IServiceAppointmentService
    {
        public ServiceAppointmentService(IRepository<ServiceAppointment> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }
    }
}
