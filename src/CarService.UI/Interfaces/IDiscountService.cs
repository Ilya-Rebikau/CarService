using CarService.UI.Models;

namespace CarService.UI.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountViewModel>> GetDiscounts(string token);
        Task CreateDiscount(string token, DiscountViewModel discountViewModel);
        Task<DiscountViewModel> GetDiscountById(string token, int id);
        Task EditDiscount(string token, int id, DiscountViewModel discountViewModel);
        Task DeleteDiscount(string token, int id);
    }
}
