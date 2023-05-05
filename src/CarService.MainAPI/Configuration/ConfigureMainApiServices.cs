using CarService.DAL.Configuration;
using CarService.MainAPI.Automapper;
using CarService.MainAPI.Interfaces;
using CarService.MainAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestEase;

namespace CarService.MainAPI.Configuration
{
    public static class ConfigureMainApiServices
    {
        public static IServiceCollection AddMainApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDalServices(connection);
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IServiceAppointmentService, ServiceAppointmentService>();
            services.AddScoped<ICarBrandService, CarBrandService>();
            services.AddScoped<ICarTypeService, CarTypeService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddHttpClient();
            services.AddTransient(scope =>
            {
                var baseUrl = configuration["UserApiAddress"];
                return RestClient.For<IUserClient>(baseUrl);
            });
            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MainAPI", Version = "v1" });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    },
                };
                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() },
                });
            });

            return services;
        }
    }
}
