namespace CarService.UI.Models.Users
{
    public class EditUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Photo { get; set; }
    }
}
