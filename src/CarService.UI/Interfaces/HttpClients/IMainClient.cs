using CarService.UI.Models;
using CarService.UI.Models.Services;
using RestEase;

namespace CarService.UI.Interfaces.HttpClients
{
    public interface IMainClient
    {
        private const string AuthorizationKey = "Authorization";

        [Get("service/getservices")]
        public Task<IEnumerable<ServiceViewModel>> GetServiceViewModels([Header(AuthorizationKey)] string token, [Query] int serviceDataId, CancellationToken cancellationToken = default);

        [Get("service/details/{id}")]
        public Task<ServiceViewModel> GetServiceById([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Post("service/create")]
        public Task CreateService([Header(AuthorizationKey)] string token, [Body] ServiceViewModel model, CancellationToken cancellationToken = default);

        [Put("service/edit/{id}")]
        public Task EditService([Header(AuthorizationKey)] string token, [Path] int id, [Body] ServiceViewModel model, CancellationToken cancellationToken = default);

        [Delete("service/delete/{id}")]
        public Task DeleteService([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("carbrand/getcarbrands")]
        public Task<IEnumerable<CarBrandViewModel>> GetCarBrandViewModels([Header(AuthorizationKey)] string token, [Query] int pageNumber, CancellationToken cancellationToken = default);

        [Get("carbrand/getallcarbrands")]
        public Task<IEnumerable<CarBrandViewModel>> GetCarBrandViewModels([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);

        [Post("carbrand/create")]
        public Task CreateCarBrand([Header(AuthorizationKey)] string token, [Body] CarBrandViewModel model, CancellationToken cancellationToken = default);

        [Get("carbrand/edit/{id}")]
        public Task<CarBrandViewModel> GetCarBrandViewModelForEdit([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Put("carbrand/edit/{id}")]
        public Task EditCarBrand([Header(AuthorizationKey)] string token, [Path] int id, [Body] CarBrandViewModel model, CancellationToken cancellationToken = default);

        [Delete("carbrand/delete/{id}")]
        public Task DeleteCarBrand([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("cartype/getcartypes")]
        public Task<IEnumerable<CarType>> GetCarTypes([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);
        
        [Get("servicedata/getservicedatas")]
        public Task<IEnumerable<ServiceDataViewModel>> GetServiceDatas([Header(AuthorizationKey)] string token, [Query] int pageNumber, CancellationToken cancellationToken = default);

        [Get("servicedata/getallservicedatas")]
        public Task<IEnumerable<ServiceDataViewModel>> GetServiceDatas([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);

        [Get("servicedata/details/{id}")]
        public Task<ServiceDataViewModel> GetServiceDataById([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);
        
        [Post("servicedata/create")]
        public Task CreateServiceData([Header(AuthorizationKey)] string token, [Body] ServiceDataViewModel model, CancellationToken cancellationToken = default);
        
        [Put("servicedata/edit/{id}")]
        public Task EditServiceData([Header(AuthorizationKey)] string token, [Path] int id, [Body] ServiceDataViewModel model, CancellationToken cancellationToken = default);
        
        [Delete("servicedata/delete/{id}")]
        public Task DeleteServiceData([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("discount/getdiscounts")]
        public Task<IEnumerable<DiscountViewModel>> GetDiscounts([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);

        [Get("discount/details/{id}")]
        public Task<DiscountViewModel> GetDiscountById([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);
        
        [Post("discount/create")]
        public Task CreateDiscount([Header(AuthorizationKey)] string token, [Body] DiscountViewModel model, CancellationToken cancellationToken = default);

        [Put("discount/edit/{id}")]
        public Task EditDiscount([Header(AuthorizationKey)] string token, [Path] int id, [Body] DiscountViewModel model, CancellationToken cancellationToken = default);

        [Delete("discount/delete/{id}")]
        public Task DeleteDiscount([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("promocode/getpromocodes")]
        public Task<IEnumerable<PromocodeViewModel>> GetPromocodes([Header(AuthorizationKey)] string token, [Query] string userId, CancellationToken cancellationToken = default);

        [Post("promocode/usepromocode")]
        public Task UsePromocode([Header(AuthorizationKey)] string token, [Query] string text, CancellationToken cancellationToken = default);

        [Post("promocode/create")]
        Task CreatePromocode([Header(AuthorizationKey)] string token, [Body] PromocodeViewModel promocode, CancellationToken cancellationToken = default);
    }
}