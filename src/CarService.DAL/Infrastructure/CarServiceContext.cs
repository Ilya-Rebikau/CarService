using CarService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CarService.DAL.Infrastructure
{
    internal class CarServiceContext : DbContext
    {
        public CarServiceContext(DbContextOptions<CarServiceContext> options)
            : base(options)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceAppointment> ServicesAppointments { get; set; }
    }
}
