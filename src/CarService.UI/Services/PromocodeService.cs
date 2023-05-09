using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;

namespace CarService.UI.Services
{
    internal class PromocodeService : IPromocodeService
    {
        private readonly IMainClient _mainClient;
        public PromocodeService(IMainClient mainClient)
        {
            _mainClient = mainClient;
        }

        public async Task Create(string token, PromocodeViewModel promocode)
        {
            await _mainClient.CreatePromocode(token, promocode);
        }

        public async Task<IEnumerable<PromocodeViewModel>> GetAllByUser(string token, string userId)
        {
            return await _mainClient.GetPromocodes(token, userId);
        }

        public async Task UsePromocode(string token, string text)
        {
            await _mainClient.UsePromocode(token, text);
        }
    }
}
