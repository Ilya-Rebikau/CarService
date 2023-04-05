using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class ChangePasswordInPersonalAccountViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "FieldRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "FieldRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "FieldRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "NewPasswordConfirmation")]
        [Compare("NewPassword", ErrorMessage = "Passwords are different")]
        public string NewPasswordConfirmation { get; set; }
    }
}
