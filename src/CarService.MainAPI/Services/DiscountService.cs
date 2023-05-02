using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class DiscountService : BaseService<Discount>, IDiscountService
    {
        public DiscountService(IRepository<Discount> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }
    }
}
