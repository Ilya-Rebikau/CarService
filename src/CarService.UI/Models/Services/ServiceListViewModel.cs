using CarService.UI.Models.CarBrands;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarService.UI.Models.Services
{
    public class ServiceListViewModel
    {
        public IEnumerable<ServiceViewModel> Services { get; set; }
        public IEnumerable<CarBrand> CarBrands { get; set; }
        public SelectList CarBrandSelectList { get; set; }
        public IEnumerable<CarType> CarTypes { get; set; }
        public SelectList CarTypeSelectList { get; set; }
    }
}
