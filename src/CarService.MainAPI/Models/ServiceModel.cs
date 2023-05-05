using CarService.DAL.Models;

namespace CarService.MainAPI.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MinutesSpent { get; set; }
        public int CarBrandId { get; set; }
        public int CarTypeId { get; set; }
        public byte[] ImageData { get; set; }
        public IEnumerable<CarBrand> CarBrands { get; set; }
        public IEnumerable<CarType> CarTypes { get; set; }
    }
}
