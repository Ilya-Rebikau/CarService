using CarService.UI.Models;
using CarService.UI.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace CarService.UI.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterViewModel model, HttpContext httpContext);

        Task LoginUser(LoginViewModel model, HttpContext httpContext);

        Task Logout(HttpContext httpContext);

        Task<User> GetUserFromJwt(string token);

        Task<EditAccountViewModel> GetEditAccountViewModelForEdit(HttpContext httpContext, string id);

        Task<IdentityResult> UpdateUserInEdit(HttpContext httpContext, EditAccountViewModel model);
    }
}
