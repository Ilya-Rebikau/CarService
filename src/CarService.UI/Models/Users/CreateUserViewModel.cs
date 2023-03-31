using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Users
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "FieldRequired")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FieldRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
