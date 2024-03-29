﻿using CarService.UserAPI.Models.Account;
using CarService.UserAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CarService.UserAPI.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResultModel> RegisterUser(RegisterModel model);
        Task<LoginResultModel> Login(LoginModel model);
        Task Logout();
        Task<EditAccountModel> GetEditAccountViewModelForEdit(string id);
        Task<IdentityResult> UpdateUserInEdit(EditAccountModel model);
        Task<AccountModel> GetAccountModel(string token);
        Task<IdentityResult> ChangePassword(ChangePasswordInPersonalAccountModel model);
        Task<string> GetUserEmail(string userId);
        Task<string> GetUserName(string userId);
        Task<byte[]> GetUserPhoto(string userId);
    }
}
