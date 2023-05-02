namespace CarService.UserAPI.Models
{
    public class JwtSettings
    {
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string JwtSecretKey { get; set; }
    }
}
