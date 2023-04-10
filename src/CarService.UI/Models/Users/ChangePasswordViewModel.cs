using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Users
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }
    }
}
