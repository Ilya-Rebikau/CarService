using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Users
{
    public class CreateUserViewModel
    {
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
