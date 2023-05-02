namespace CarService.UserAPI.Models.Users
{
    public class ChangePasswordModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
