using CarService.DAL.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface IAppointmentService : IBaseService<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllByDateAndServiceData(DateTime date, int serviceId);
        IEnumerable<Appointment> GetAllByUserId(string userId, int pageNumber);
    }
}
