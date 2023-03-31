using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }
    }
}
