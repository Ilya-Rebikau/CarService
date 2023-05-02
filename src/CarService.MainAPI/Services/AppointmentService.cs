using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        public AppointmentService(IRepository<Appointment> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }
    }
}
