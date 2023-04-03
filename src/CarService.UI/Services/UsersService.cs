using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;
using CarService.UI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace CarService.UI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersClient _client;

        public UsersService(IUsersClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<User>> GetUsers(HttpContext httpContext, int pageNumber)
        {
            return await _client.GetUsers(httpContext.GetJwtToken(), pageNumber);
        }

        public async Task<IdentityResult> CreateUser(HttpContext httpContext, CreateUserViewModel model)
        {
            return await _client.CreateUser(httpContext.GetJwtToken(), model);
        }

        public async Task<EditUserViewModel> GetEditUserViewModel(HttpContext httpContext, string id)
        {
            return await _client.GetEditUserViewModel(httpContext.GetJwtToken(), id);
        }

        public async Task<IdentityResult> EditUser(HttpContext httpContext, EditUserViewModel model)
        {
            return await _client.EditUser(httpContext.GetJwtToken(), model);
        }

        public async Task<string> DeleteUser(HttpContext httpContext, string id)
        {
            return await _client.DeleteUser(httpContext.GetJwtToken(), id);
        }

        public async Task<ChangePasswordViewModel> GetChangePasswordViewModel(HttpContext httpContext, string id)
        {
            return await _client.GetChangePasswordViewModel(httpContext.GetJwtToken(), id);
        }

        public async Task<IdentityResult> ChangePassowrd(HttpContext httpContext, ChangePasswordViewModel model)
        {
            return await _client.ChangePassword(httpContext.GetJwtToken(), model);
        }

        public async Task<ChangeRoleViewModel> GetChangeRoleViewModel(HttpContext httpContext, string id)
        {
            return await _client.GetChangeRoleViewModel(httpContext.GetJwtToken(), id);
        }

        public async Task ChangeRoles(HttpContext httpContext, ChangeRoleViewModel model)
        {
            await _client.ChangeRoles(httpContext.GetJwtToken(), model);
        }
    }
}
