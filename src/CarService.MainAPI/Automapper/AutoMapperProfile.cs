﻿using AutoMapper;
using CarService.DAL.Models;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Automapper
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Service, ServiceModel>().ReverseMap();
        }
    }
}
