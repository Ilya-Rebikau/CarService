using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "FieldRequired")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FieldRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
