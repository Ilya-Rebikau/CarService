using CarService.UserAPI.Interfaces;
using CarService.UserAPI.Models.Users;
using CarService.UserAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarService.UserAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly string _baseUserRole;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConverter<User, EditUserModel> _converter;
        private readonly int _countOnPage;

        public UsersService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConverter<User, EditUserModel> converter,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _converter = converter;
            _baseUserRole = configuration.GetValue<string>("BaseRole");
            _countOnPage = configuration.GetValue<int>("CountOnPage");
        }

        public async Task<IdentityResult> UpdateUserInEdit(EditUserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            user.Email = model.Email;
            user.UserName = model.Email;
            user.FirstName = model.FirstName;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.Photo = model.Photo;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<string> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains("admin"))
            {
                throw new ValidationException("Вы не можете удалить аккаунт админа.");
            }

            await _userManager.DeleteAsync(user);
            return id;
        }

        public async Task<ChangeRoleModel> GetChangeRoleViewModel(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            var model = new ChangeRoleModel
            {
                UserId = user.Id,
                UserEmail = user.UserName,
                UserRoles = userRoles,
                AllRoles = allRoles,
            };
            return model;
        }

        public async Task EditRoles(ChangeRoleModel model)
        {
            model.AllRoles = _roleManager.Roles.ToList();
            var user = await _userManager.FindByIdAsync(model.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = model.UserRoles.Except(userRoles).ToList();
            var removedRoles = userRoles.Except(model.UserRoles).ToList();
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
        }

        public Task<IEnumerable<User>> GetUsers(int pageNumber)
        {
            var users = _userManager.Users;
            CheckForPageNumber(pageNumber);
            return Task.FromResult(users.OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * _countOnPage).Take(_countOnPage).AsEnumerable());
        }

        public async Task<IdentityResult> CreateUser(CreateUserModel model)
        {
            var user = new User { Email = model.Email, UserName = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, _baseUserRole);
            }

            return result;
        }

        public async Task<EditUserModel> GetEditUserViewModel(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return await _converter.ConvertSourceToDestination(user);
        }

        public async Task<ChangePasswordModel> GetChangePasswordViewModel(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var model = new ChangePasswordModel
            {
                Id = user.Id,
                Email = user.Email,
            };
            return model;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel model, HttpContext httpContext)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user is null)
            {
                throw new ValidationException("Пользователь не найден");
            }

            var passwordValidator = httpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
            var passwordHasher = httpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;
            var result = await passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
            if (result.Succeeded)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, model.NewPassword);
                await _userManager.UpdateAsync(user);
            }

            return result;
        }

        private static void CheckForPageNumber(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                throw new ValidationException("Номер страницы должен быть положительным!");
            }
        }
    }
}
