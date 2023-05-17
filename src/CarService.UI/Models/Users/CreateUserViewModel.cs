using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Users
{
    public class CreateUserViewModel
    {
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, ErrorMessage = "Длина должна быть до 30 символов")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Длина должна быть от 4 до 30 символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
