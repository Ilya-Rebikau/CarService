using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода!")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }
    }
}
