using AutoMapper;
using CarService.UserAPI.Models;
using CarService.UserAPI.Models.Account;
using CarService.UserAPI.Models.Users;

namespace CarService.UserAPI.Automapper
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, EditAccountModel>();
            CreateMap<User, AccountModel>();
            CreateMap<User, EditUserModel>();
        }
    }
}
