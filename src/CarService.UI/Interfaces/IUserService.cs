using CarService.UI.Models;
using CarService.UI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace CarService.UI.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers(string token, int pageNumber);

        Task<IdentityResult> CreateUser(string token, CreateUserViewModel model);

        Task<EditUserViewModel> GetEditUserViewModel(string token, string id);

        Task<IdentityResult> EditUser(string token, EditUserViewModel model);

        Task<string> DeleteUser(string token, string id);

        Task<ChangePasswordViewModel> GetChangePasswordViewModel(string token, string id);

        Task<IdentityResult> ChangePassowrd(string token, ChangePasswordViewModel model);

        Task<ChangeRoleViewModel> GetChangeRoleViewModel(string token, string id);

        Task ChangeRoles(string token, ChangeRoleViewModel model);
    }
}
