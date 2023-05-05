using System.ComponentModel.DataAnnotations;
using CarService.UI.Models.CarBrands;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarService.UI.Models.Services
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Услуга")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Приблизительное время ремонта, мин")]
        public int MinutesSpent { get; set; }
        public int CarBrandId { get; set; }
        public int CarTypeId { get; set; }
        public byte[] ImageData { get; set; }

        [Display(Name = "Картинка")]
        public IFormFile Image { get; set; }
        public IEnumerable<CarBrand> CarBrands { get; set; }
        public IEnumerable<CarType> CarTypes { get; set; }
        public SelectList CarBrandSelectList { get; set; }
        public SelectList CarTypeSelectList { get; set; }
    }
}