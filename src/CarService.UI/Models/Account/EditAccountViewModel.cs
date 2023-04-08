using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class EditAccountViewModel
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

        [Display(Name = "Удалить текущее фото?")]
        public bool DeletePhoto { get; set; }
    }
}
