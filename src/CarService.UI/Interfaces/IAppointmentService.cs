using CarService.UI.Models.Appointment;

namespace CarService.UI.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentViewModel>> GetAllAppointments(string token, int pageNumber);
        Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByDateAndService(string token, string dateTime, int serviceId);
        Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByUser(string token, string userId);
        Task CreateAppointment(string token,  AppointmentViewModel model);
        Task DeleteAppointment(string token, int id);
        Task FinishAppointment(string token, int id);
    }
}
