using CarService.DAL.Models;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface IAppointmentService : IBaseService<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllByDateAndServiceData(DateTime date, int serviceId);
        Task<IEnumerable<AppointmentModel>> GetAllByUserId(string userId);
        Task<IEnumerable<AppointmentModel>> GetAllAppointments(string token, int pageNumber);
        Task FinishAppointment(int id);
    }
}
