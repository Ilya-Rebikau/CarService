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

        public async Task<IEnumerable<User>> GetUsers(string token, int pageNumber)
        {
            return await _client.GetUsers(token, pageNumber);
        }

        public async Task<IdentityResult> CreateUser(string token, CreateUserViewModel model)
        {
            return await _client.CreateUser(token, model);
        }

        public async Task<EditUserViewModel> GetEditUserViewModel(string token, string id)
        {
            var editModel = await _client.GetEditUserModel(token, id);
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

        public async Task<IdentityResult> EditUser(string token, EditUserViewModel model)
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

            return await _client.EditUser(token, editUserModel);
        }

        public async Task<string> DeleteUser(string token, string id)
        {
            return await _client.DeleteUser(token, id);
        }

        public async Task<ChangePasswordViewModel> GetChangePasswordViewModel(string token, string id)
        {
            return await _client.GetChangePasswordViewModel(token, id);
        }

        public async Task<IdentityResult> ChangePassowrd(string token, ChangePasswordViewModel model)
        {
            return await _client.ChangePassword(token, model);
        }

        public async Task<ChangeRoleViewModel> GetChangeRoleViewModel(string token, string id)
        {
            return await _client.GetChangeRoleViewModel(token, id);
        }

        public async Task ChangeRoles(string token, ChangeRoleViewModel model)
        {
            await _client.ChangeRoles(token, model);
        }
    }
}
