using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, ErrorMessage = "Длина должна быть до 30 символов")]
        [Required(ErrorMessage = "E-mail обязателен для ввода")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Длина должна быть от 4 до 30 символов")]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязателен для ввода")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Длина должна быть от 4 до 30 символов")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Повторите пароль")]
        public string PasswordConfirm { get; set; }
    }
}
