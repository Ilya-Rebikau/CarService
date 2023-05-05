using CarService.DAL.Models;

namespace CarService.MainAPI.Models
{
    public class ServiceListModel
    {
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<CarBrand> CarBrands { get; set; }
        public IEnumerable<CarType> CarTypes { get; set; }
    }
}
