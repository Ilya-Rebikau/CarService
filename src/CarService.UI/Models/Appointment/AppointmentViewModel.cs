using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Appointment
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата и время записи")]
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }

        [Display(Name = "Дополнительное сообщение")]
        public string Message { get; set; }
        public string UserId { get; set; }
        public bool WasFinished { get; set; }
        public int ServiceId { get; set; }
    }
}
