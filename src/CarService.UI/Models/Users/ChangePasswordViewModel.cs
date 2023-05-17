using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Users
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Длина должна быть от 4 до 30 символов")]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }
    }
}
