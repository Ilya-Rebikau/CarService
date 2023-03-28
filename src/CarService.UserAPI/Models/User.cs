using Microsoft.AspNetCore.Identity;

namespace CarService.UserAPI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
