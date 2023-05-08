using CarService.DAL.Models;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface IDiscountService : IBaseService<Discount>
    {
        Task<IEnumerable<DiscountModel>> GetAllDiscountModels();
        Task<DiscountModel> GetDiscountModelById(int id);
        Task CreateDiscountModel(DiscountModel model);
        Task UpdateDiscountModel(DiscountModel model);
    }
}
