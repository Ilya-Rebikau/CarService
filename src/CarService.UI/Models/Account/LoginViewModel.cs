using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода!")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
