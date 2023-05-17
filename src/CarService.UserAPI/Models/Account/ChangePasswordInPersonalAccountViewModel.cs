using System.ComponentModel.DataAnnotations;

namespace CarService.UserAPI.Models.Account
{
    public class ChangePasswordInPersonalAccountModel
    {
        public string Id { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }
    }
}
