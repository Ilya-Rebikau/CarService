using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public override string  Email { get; set; }

        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public override string PhoneNumber { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }
    }
}
