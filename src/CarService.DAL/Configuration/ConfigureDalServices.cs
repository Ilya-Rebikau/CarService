using CarService.DAL.Infrastructure;
using CarService.DAL.Interfaces;
using CarService.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.DAL.Configuration
{
    public static class ConfigureDalServices
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services, string connection)
        {
            services.AddDbContext<CarServiceContext>(options =>
                options.UseSqlServer(connection));
            services.AddScoped<DbContext, CarServiceContext>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
