using System.ComponentModel.DataAnnotations;

namespace CarService.UserAPI.Models.Account
{
    public class AccountModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }
}
