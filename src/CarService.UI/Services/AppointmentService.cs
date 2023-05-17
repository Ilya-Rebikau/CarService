using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models.Appointment;

namespace CarService.UI.Services
{
    internal class AppointmentService : IAppointmentService
    {
        private readonly IMainClient _mainClient;
        public AppointmentService(IMainClient mainClient)
        {
            _mainClient = mainClient;
        }

        public async Task CreateAppointment(string token, AppointmentViewModel model)
        {
            await _mainClient.CreateAppointment(token, model);
        }

        public async Task DeleteAppointment(string token, int id)
        {
            await _mainClient.DeleteAppointment(token, id);
        }

        public async Task FinishAppointment(string token, int id)
        {
            await _mainClient.FinishAppointment(token, id);
        }

        public async Task<IEnumerable<AppointmentViewModel>> GetAllAppointments(string token, int pageNumber)
        {
            return await _mainClient.GetAllAppointments(token, pageNumber);
        }

        public async Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByDateAndService(string token, string dateTime, int serviceId)
        {
            return await _mainClient.GetAppointmentsByDateAndService(token, dateTime, serviceId);
        }

        public async Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByUser(string token, string userId)
        {
            return await _mainClient.GetAppointmentsByUser(token, userId);
        }
    }
}
