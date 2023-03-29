using CarService.UserAPI.Models.Users;
using CarService.UserAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CarService.UserAPI.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsers(int pageNumber);
        Task<IdentityResult> CreateUser(CreateUserModel model);
        Task<ChangePasswordModel> GetChangePasswordViewModel(string id);
        Task<IdentityResult> ChangePassword(ChangePasswordModel model, HttpContext httpContext);
        Task<EditUserModel> GetEditUserViewModel(string id);
        Task<IdentityResult> UpdateUserInEdit(EditUserModel model);
        Task<string> DeleteUser(string id);
        Task<ChangeRoleModel> GetChangeRoleViewModel(string id);
        Task EditRoles(ChangeRoleModel model);
    }
}
