using CarService.UI.Models.Appointment;

namespace CarService.UI.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByDateAndService(string token, string dateTime, int serviceId);
        Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByUser(string token, string userId, int pageNumber);
        Task CreateAppointment(string token,  AppointmentViewModel model);
    }
}
