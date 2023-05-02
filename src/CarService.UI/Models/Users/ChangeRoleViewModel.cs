using Microsoft.AspNetCore.Identity;

namespace CarService.UI.Models.Users
{
    public class ChangeRoleViewModel
    {
        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public IList<IdentityRole> AllRoles { get; set; }

        public IList<string> UserRoles { get; set; }
    }
}
