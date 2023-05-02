using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarService.UserAPI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public byte[] Photo { get; set; }
    }
}
