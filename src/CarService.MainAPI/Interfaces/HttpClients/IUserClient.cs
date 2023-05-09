using RestEase;

namespace CarService.MainAPI.Interfaces.HttpClients
{
    public interface IUserClient
    {
        private const string AuthorizationKey = "Authorization";

        [Post("account/validatetoken")]
        public Task<bool> ValidateToken([Header(AuthorizationKey)] string token);
    }
}
