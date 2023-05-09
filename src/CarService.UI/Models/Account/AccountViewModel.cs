using CarService.UI.Models.Appointment;
using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class AccountViewModel
    {
        public string Id { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        public IEnumerable<PromocodeViewModel> Promocodes { get; set; }

        public IEnumerable<AppointmentViewModel> Appointments { get; set; }

        public byte[] Photo { get; set; }
    }
}