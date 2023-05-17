using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Appointment
{
    public class AppointmentListViewModel
    {
        public IEnumerable<AppointmentViewModel> Appointments { get; set; }
        public AppointmentViewModel NewAppointment { get; set; }
        public DateTime Date { get; set; }
    }
}
