using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Appointment
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата и время записи")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStart { get; set; }

        [Display(Name = "Ожидаемое дата и время конца ремонта")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeEnd { get; set; }

        [Display(Name = "Дополнительное сообщение")]
        public string Message { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Закончено?")]
        public bool WasFinished { get; set; }
        public int ServiceId { get; set; }

        [Display(Name = "Услуга")]
        public string ServiceData { get; set; }

        [Display(Name = "Почта клиента")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
    }
}
