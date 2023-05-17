using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class ChangePasswordInPersonalAccountViewModel
    {
        public string Id { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Длина должна быть от 4 до 30 символов")]
        [Display(Name = "Старый пароль")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Длина должна быть от 4 до 30 символов")]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Длина должна быть от 4 до 30 символов")]
        [Compare(nameof(NewPassword), ErrorMessage = "Пароли не совпадают")]
        public string NewPasswordConfirmation { get; set; }
    }
}
