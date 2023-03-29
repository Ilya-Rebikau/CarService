using CarService.UserAPI.Models.Account;
using CarService.UserAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CarService.UserAPI.Interfaces
{
    public interface IAccountService
    {
        Task<User> GetUser(string jwtToken);
        Task<RegisterResultModel> RegisterUser(RegisterModel model);
        Task<LoginResultModel> Login(LoginModel model);
        Task Logout();
        Task<EditAccountModel> GetEditAccountViewModelForEdit(string id);
        Task<IdentityResult> UpdateUserInEdit(EditAccountModel model);
        Task<string> GetUserId(string token);
    }
}
