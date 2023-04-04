using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;
using CarService.UI.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace CarService.UI.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IUsersClient _userClient;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUsersClient userClient)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userClient = userClient;
        }

        public async Task RegisterUser(RegisterViewModel model, HttpContext httpContext)
        {
            var token = await _userClient.Register(model);
            await SignIn(token, httpContext);
        }

        public async Task LoginUser(LoginViewModel model, HttpContext httpContext)
        {
            var token = await _userClient.Login(model);
            await SignIn(token, httpContext);
        }

        public async Task Logout(HttpContext httpContext)
        {
            await _userClient.Logout(httpContext.GetJwtToken());
            await _signInManager.SignOutAsync();
            httpContext.DeleteCookies();
        }

        public async Task<EditAccountViewModel> GetEditAccountViewModelForEdit(HttpContext httpContext, string id)
        {
            return await _userClient.Edit(httpContext.GetJwtToken(), id);
        }

        public async Task<IdentityResult> UpdateUserInEdit(HttpContext httpContext, EditAccountViewModel model)
        {
            return await _userClient.Edit(httpContext.GetJwtToken(), model);
        }

        private async Task SignIn(string token, HttpContext httpContext)
        {
            if (string.IsNullOrEmpty(token) || httpContext is null)
            {
                throw new InvalidOperationException("Wrong jwt token or http context");
            }

            httpContext.AppendCookies(token);
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var user = await _userManager.FindByEmailAsync(jwtSecurityToken.Subject);
            await _signInManager.SignInAsync(user, false);
        }

        public async Task<AccountViewModel> GetAccountViewModel(HttpContext httpContext)
        {
            return await _userClient.GetAccountViewModel(httpContext.GetJwtToken());
        }
    }
}
