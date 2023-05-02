using CarService.UI.Models;
using CarService.UI.Models.Account;
using CarService.UI.Models.Users;
using Microsoft.AspNetCore.Identity;
using RestEase;

namespace CarService.UI.Interfaces.HttpClients
{
    public interface IUsersClient
    {
        private const string AuthorizationKey = "Authorization";

        [Post("account/register")]
        public Task<string> Register([Body] RegisterViewModel userModel, CancellationToken cancellationToken = default);

        [Post("account/login")]
        public Task<string> Login([Body] LoginViewModel userModel, CancellationToken cancellationToken = default);

        [Post("account/logout")]
        public Task Logout([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);

        [Get("account/edit/{id}")]
        public Task<EditAccountModel> Edit([Header(AuthorizationKey)] string token, [Path] string id, CancellationToken cancellationToken = default);

        [Put("account/edit")]
        public Task<IdentityResult> Edit([Header(AuthorizationKey)] string token, [Body] EditAccountModel model, CancellationToken cancellationToken = default);

        [Get("account/getaccountmodel")]
        public Task<AccountViewModel> GetAccountViewModel([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);

        [Post("account/changepassword")]
        public Task<IdentityResult> ChangePassword([Header(AuthorizationKey)] string token, [Body] ChangePasswordInPersonalAccountViewModel model, CancellationToken cancellationToken = default);
        
        [Get("user/getusers")]
        public Task<IEnumerable<User>> GetUsers([Header(AuthorizationKey)] string token, [Body] int pageNumber, CancellationToken cancellationToken = default);

        [Post("user/create")]
        public Task<IdentityResult> CreateUser([Header(AuthorizationKey)] string token, [Body] CreateUserViewModel model, CancellationToken cancellationToken = default);

        [Get("user/edit/{id}")]
        public Task<EditUserModel> GetEditUserModel([Header(AuthorizationKey)] string token, [Path] string id, CancellationToken cancellationToken = default);

        [Put("user/edit")]
        public Task<IdentityResult> EditUser([Header(AuthorizationKey)] string token, [Body] EditUserModel model, CancellationToken cancellationToken = default);

        [Delete("user/delete/{id}")]
        public Task<string> DeleteUser([Header(AuthorizationKey)] string token, [Path] string id, CancellationToken cancellationToken = default);

        [Get("user/changepassword/{id}")]
        public Task<ChangePasswordViewModel> GetChangePasswordViewModel([Header(AuthorizationKey)] string token, [Path] string id, CancellationToken cancellationToken = default);

        [Put("user/changepassword")]
        public Task<IdentityResult> ChangePassword([Header(AuthorizationKey)] string token, [Body] ChangePasswordViewModel model, CancellationToken cancellationToken = default);

        [Get("user/editroles/{id}")]
        public Task<ChangeRoleViewModel> GetChangeRoleViewModel([Header(AuthorizationKey)] string token, [Path] string id, CancellationToken cancellationToken = default);

        [Put("user/editroles")]
        public Task ChangeRoles([Header(AuthorizationKey)] string token, [Body] ChangeRoleViewModel model, CancellationToken cancellationToken = default);
    }
}