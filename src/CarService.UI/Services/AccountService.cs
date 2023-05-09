using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;
using CarService.UI.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace CarService.UI.Services
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IUserClient _userClient;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserClient userClient)
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
            await _userClient.Logout(httpContext.GetJwt());
            await _signInManager.SignOutAsync();
            httpContext.DeleteCookies();
        }

        public async Task<EditAccountViewModel> GetEditAccountViewModelForEdit(HttpContext httpContext, string id)
        {
            var editModel = await _userClient.Edit(httpContext.GetJwt(), id);
            var editAccountViewModel = new EditAccountViewModel
            {
                Id = editModel.Id,
                FirstName = editModel.FirstName,
                Surname = editModel.Surname,
                PhoneNumber = editModel.PhoneNumber,
                Email = editModel.Email,
                PhotoData = editModel.Photo
            };
            return editAccountViewModel;
        }

        public async Task<IdentityResult> UpdateUserInEdit(HttpContext httpContext, EditAccountViewModel model)
        {
            var editAccountModel = new EditAccountModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                Surname = model.Surname,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };
            if (model.DeletePhoto)
            {
                editAccountModel.Photo = null;
            }
            else if (model.Photo is not null)
            {
                using var binaryReader = new BinaryReader(model.Photo.OpenReadStream());
                byte[] photoData = binaryReader.ReadBytes((int)model.Photo.Length);
                editAccountModel.Photo = photoData;
            }
            else
            {
                editAccountModel.Photo = model.PhotoData;
            }

            return await _userClient.Edit(httpContext.GetJwt(), editAccountModel);
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
            return await _userClient.GetAccountViewModel(httpContext.GetJwt());
        }

        public async Task<IdentityResult> ChangePassword(HttpContext httpContext, ChangePasswordInPersonalAccountViewModel model)
        {
            return await _userClient.ChangePassword(httpContext.GetJwt(), model);
        }
    }
}
