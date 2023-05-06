using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;

namespace CarService.UI.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IMainClient _mainClient;
        public DiscountService(IMainClient mainClient)
        {
            _mainClient = mainClient;
        }

        public async Task CreateDiscount(string token, DiscountViewModel discountViewModel)
        {
            await _mainClient.CreateDiscount(token, discountViewModel);
        }

        public async Task<IEnumerable<DiscountViewModel>> GetDiscounts(string token)
        {
            return await _mainClient.GetDiscounts(token);
        }
    }
}
