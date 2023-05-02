using RestEase;

namespace CarService.MainAPI.Interfaces
{
    public interface IUsersClient
    {
        private const string AuthorizationKey = "Authorization";

        [Post("account/validatetoken")]
        public Task<bool> ValidateToken([Header(AuthorizationKey)] string token);
    }
}
