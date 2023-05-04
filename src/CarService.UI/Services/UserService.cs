using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;
using CarService.UI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace CarService.UI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserClient _client;

        public UserService(IUserClient client)
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
            var editModel = await _client.GetEditUserModel(httpContext.GetJwtToken(), id);
            var editUserViewModel = new EditUserViewModel
            {
                Id = editModel.Id,
                FirstName = editModel.FirstName,
                Surname = editModel.Surname,
                PhoneNumber = editModel.PhoneNumber,
                Email = editModel.Email,
                PhotoData = editModel.Photo
            };
            return editUserViewModel;
        }

        public async Task<IdentityResult> EditUser(HttpContext httpContext, EditUserViewModel model)
        {
            var editUserModel = new EditUserModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                Surname = model.Surname,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Photo = null
            };
            if (model.DeletePhoto)
            {
                editUserModel.Photo = null;
            }
            else if (model.Photo is not null)
            {
                using var binaryReader = new BinaryReader(model.Photo.OpenReadStream());
                byte[] photoData = binaryReader.ReadBytes((int)model.Photo.Length);
                editUserModel.Photo = photoData;
            }
            else
            {
                editUserModel.Photo = model.PhotoData;
            }

            return await _client.EditUser(httpContext.GetJwtToken(), editUserModel);
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
