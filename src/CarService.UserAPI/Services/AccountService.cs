using CarService.UserAPI.Interfaces;
using CarService.UserAPI.Models.Account;
using CarService.UserAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;

namespace CarService.UserAPI.Services
{
    internal class AccountService : IAccountService
    {
        private readonly string _baseRoleForNewUser;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _baseRoleForNewUser = configuration.GetValue<string>("BaseRole");
            _mapper = mapper;
        }

        public async Task<RegisterResultModel> RegisterUser(RegisterModel model)
        {
            var user = new User { Email = model.Email, UserName = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, _baseRoleForNewUser);
            IList<string> roles = new List<string>();
            if (result.Succeeded)
            {
                user = await _userManager.FindByEmailAsync(model.Email);
                roles = await _userManager.GetRolesAsync(user);
                await _signInManager.SignInAsync(user, false);
            }

            var registerResult = new RegisterResultModel
            {
                User = user,
                Roles = roles,
                IdentityResult = result,
            };
            return registerResult;
        }

        public async Task<LoginResultModel> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            var loginResult = new LoginResultModel
            {
                SignInResult = result,
            };
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                loginResult.User = user;
                loginResult.Roles = await _userManager.GetRolesAsync(user);
            }

            return loginResult;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<EditAccountModel> GetEditAccountViewModelForEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<User, EditAccountModel>(user);
        }

        public async Task<IdentityResult> UpdateUserInEdit(EditAccountModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            user.Email = model.Email;
            user.UserName = model.Email;
            user.FirstName = model.FirstName;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.Photo = model.Photo;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<AccountModel> GetAccountModel(string token)
        {
            var user = await GetUser(token);
            return _mapper.Map<User, AccountModel>(user);
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordInPersonalAccountModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            return await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }

        private async Task<User> GetUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims;
            var identity = new ClaimsIdentity(claims);
            var principalClaims = new ClaimsPrincipal(identity);
            var email = principalClaims.FindFirst(JwtRegisteredClaimNames.Sub).Value;
            return await _userManager.FindByEmailAsync(email);
        }
    }
}