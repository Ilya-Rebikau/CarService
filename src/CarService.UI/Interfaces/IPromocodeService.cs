using CarService.UI.Models;

namespace CarService.UI.Interfaces
{
    public interface IPromocodeService
    {
        Task Create(string token, PromocodeViewModel promocode);
        Task<IEnumerable<PromocodeViewModel>> GetAllByUser(string token, string userId);
        Task UsePromocode(string token, string text);
    }
}
