using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Users
{
    public class EditUserViewModel
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

        [Display(Name = "Фото")]
        public IFormFile Photo { get; set; }
        public byte[] PhotoData { get; set; }

        [Display(Name = "Удалить фото?")]
        public bool DeletePhoto { get; set; }
    }
}
