using CarService.DAL.Models;

namespace CarService.MainAPI.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        public int MinutesSpent { get; set; }
        public int? CarBrandId { get; set; }
        public int? CarTypeId { get; set; }
        public int ServiceDataId { get; set; }
        public string CarTypeName { get; set; }
        public string CarBrandName { get; set; }
    }
}
