using AutoMapper;
using CarService.UserAPI.Models;
using CarService.UserAPI.Models.Account;

namespace CarService.UserAPI.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, EditAccountModel>().ReverseMap();
        }
    }
}
