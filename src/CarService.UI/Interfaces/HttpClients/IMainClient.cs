using CarService.UI.Models.Service;
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
    }
}