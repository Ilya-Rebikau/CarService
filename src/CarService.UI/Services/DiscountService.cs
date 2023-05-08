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

        public async Task DeleteDiscount(string token, int id)
        {
            await _mainClient.DeleteDiscount(token, id);
        }

        public async Task<DiscountViewModel> GetDiscountById(string token, int id)
        {
            return await _mainClient.GetDiscountById(token, id);
        }

        public async Task<IEnumerable<DiscountViewModel>> GetDiscounts(string token)
        {
            return await _mainClient.GetDiscounts(token);
        }

        public async Task EditDiscount(string token, int id, DiscountViewModel discountViewModel)
        {
            await _mainClient.EditDiscount(token, id, discountViewModel);
        }
    }
}
