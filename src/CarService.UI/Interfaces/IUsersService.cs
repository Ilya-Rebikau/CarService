using CarService.UI.Models;
using CarService.UI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace CarService.UI.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsers(HttpContext httpContext, int pageNumber);

        Task<IdentityResult> CreateUser(HttpContext httpContext, CreateUserViewModel model);

        Task<EditUserViewModel> GetEditUserViewModel(HttpContext httpContext, string id);

        Task<IdentityResult> EditUser(HttpContext httpContext, EditUserViewModel model);

        Task<string> DeleteUser(HttpContext httpContext, string id);

        Task<ChangePasswordViewModel> GetChangePasswordViewModel(HttpContext httpContext, string id);

        Task<IdentityResult> ChangePassowrd(HttpContext httpContext, ChangePasswordViewModel model);

        Task<ChangeRoleViewModel> GetChangeRoleViewModel(HttpContext httpContext, string id);

        Task ChangeRoles(HttpContext httpContext, ChangeRoleViewModel model);
    }
}
