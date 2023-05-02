using CarService.UserAPI.Models;

namespace CarService.UserAPI.Interfaces
{
    public interface IJwtService
    {
        string GetJwt(User user, IList<string> roles);
        bool ValidateJwt(string token);
    }
}
