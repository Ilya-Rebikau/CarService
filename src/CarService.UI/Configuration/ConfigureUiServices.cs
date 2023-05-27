using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;
using CarService.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestEase;

namespace CarService.UI.Configuration
{
    public static class ConfigureUiServices
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services, string connection, IConfiguration configuration)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ICarBrandService, CarBrandService>();
            services.AddScoped<IServiceDataService, ServiceDataService>();
            services.AddScoped<ICarTypeService, CarTypeService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IPromocodeService, PromocodeService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connection));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            services.AddHttpClient();
            services.AddScoped(scope =>
            {
                var baseUrl = configuration["UserApiAddress"];
                return RestClient.For<IUserClient>(baseUrl);
            });
            services.AddScoped(scope =>
            {
                var baseUrl = configuration["MainApiAddress"];
                return RestClient.For<IMainClient>(baseUrl);
            });
            services.AddDetection();
            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MVC", Version = "v1" });
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
