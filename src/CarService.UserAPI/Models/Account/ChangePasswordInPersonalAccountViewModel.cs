using System.ComponentModel.DataAnnotations;

namespace CarService.UserAPI.Models.Account
{
    public class ChangePasswordInPersonalAccountModel
    {
        public string Id { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords are different")]
        public string NewPasswordConfirmation { get; set; }
    }
}
