using CarService.DAL.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface IPromocodeService : IBaseService<Promocode>
    {
        IEnumerable<Promocode> GetAllByUser(string userId);
        Promocode GetPromocodeByText(string text);
    }
}
