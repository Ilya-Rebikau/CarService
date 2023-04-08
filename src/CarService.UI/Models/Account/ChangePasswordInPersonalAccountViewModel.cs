using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class ChangePasswordInPersonalAccountViewModel
    {
        public string Id { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string NewPasswordConfirmation { get; set; }
    }
}
