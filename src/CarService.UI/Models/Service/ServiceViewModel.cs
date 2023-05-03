namespace CarService.UI.Models.Service
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MinutesSpent { get; set; }
        public int CarBrandId { get; set; }
        public int CarTypeId { get; set; }
        public byte[] ImageData { get; set; }
        public IFormFile Image { get; set; }
    }
}
