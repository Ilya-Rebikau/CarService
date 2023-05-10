using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class EditAccountViewModel
    {
        public string Id { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, ErrorMessage = "Длина должна быть до 30 символов")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        [StringLength(30, ErrorMessage = "Длина должна быть до 30 символов")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(30, ErrorMessage = "Длина должна быть до 30 символов")]
        public string Surname { get; set; }

        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(30, ErrorMessage = "Длина должна быть до 30 символов")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Фото")]
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
        public byte[] PhotoData { get; set; }

        [Display(Name = "Удалить текущее фото?")]
        public bool DeletePhoto { get; set; }
    }
}
