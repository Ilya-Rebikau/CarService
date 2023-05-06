using CarService.UI.Models;

namespace CarService.UI.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountViewModel>> GetDiscounts(string token);
        Task CreateDiscount(string token, DiscountViewModel discountViewModel);
    }
}
