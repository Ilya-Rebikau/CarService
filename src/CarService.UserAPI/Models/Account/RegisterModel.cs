namespace CarService.UserAPI.Models.Account
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
