using Microsoft.AspNetCore.Identity;

namespace CarService.UserAPI.Models.Account
{
    public class RegisterResultModel
    {
        public IdentityResult IdentityResult { get; set; }
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
