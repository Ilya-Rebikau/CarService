using CarService.UI.Models;
using RestEase;

namespace CarService.UI.Interfaces.HttpClients
{
    public interface IMainClient
    {
        private const string AuthorizationKey = "Authorization";

        [Get("service/getservices")]
        public Task<IEnumerable<ServiceViewModel>> GetServiceViewModels([Header(AuthorizationKey)] string token, [Query] int pageNumber, CancellationToken cancellationToken = default);

        [Get("service/details/{id}")]
        public Task<ServiceViewModel> ServiceDetails([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Post("service/create")]
        public Task CreateService([Header(AuthorizationKey)] string token, [Body] ServiceViewModel model, CancellationToken cancellationToken = default);

        [Get("service/edit/{id}")]
        public Task<ServiceViewModel> GetServiceViewModelForEdit([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Put("service/edit/{id}")]
        public Task EditService([Header(AuthorizationKey)] string token, [Path] int id, [Body] ServiceViewModel model, CancellationToken cancellationToken = default);

        [Get("service/delete/{id}")]
        public Task<ServiceViewModel> GetServiceViewModelForDelete([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Delete("service/delete/{id}")]
        public Task DeleteService([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);
        
        [Get("carbrand/getcarbrands")]
        public Task<IEnumerable<CarBrandViewModel>> GetCarBrandViewModels([Header(AuthorizationKey)] string token, [Query] int pageNumber, CancellationToken cancellationToken = default);

        [Post("carbrand/create")]
        public Task CreateCarBrand([Header(AuthorizationKey)] string token, [Body] CarBrandViewModel model, CancellationToken cancellationToken = default);

        [Get("carbrand/edit/{id}")]
        public Task<CarBrandViewModel> GetCarBrandViewModelForEdit([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Put("carbrand/edit/{id}")]
        public Task EditCarBrand([Header(AuthorizationKey)] string token, [Path] int id, [Body] CarBrandViewModel model, CancellationToken cancellationToken = default);

        [Delete("carbrand/delete/{id}")]
        public Task DeleteCarBrand([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("cartype/getcartypes")]
        public Task<IEnumerable<CarType>> GetCarTypes([Header(AuthorizationKey)] string token, [Query] int pageNumber, CancellationToken cancellationToken = default);
    }
}