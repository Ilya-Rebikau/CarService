using Microsoft.AspNetCore.Identity;

namespace CarService.UI.Models.Account
{
    public class RegisterResultModel
    {
        public IdentityResult IdentityResult { get; set; }
        public string Token { get; set; }
    }
}
