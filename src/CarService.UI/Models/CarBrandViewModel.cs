using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models
{
    public class CarBrandViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Марка автомобиля")]
        public string Name { get; set; }
        public byte[] ImageData { get; set; }

        [Display(Name = "Лого")]
        public IFormFile Image { get; set; }
    }
}
