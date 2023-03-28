using Microsoft.AspNetCore.Identity;

namespace CarService.UserAPI.Models.Account
{
    public class LoginResultModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
        public SignInResult SignInResult { get; set; }
    }
}
