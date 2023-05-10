using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models
{
    public class CarBrandViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Марка автомобиля")]
        [StringLength(50, MinimumLength = 1, ErrorMessage ="Длина должна быть от 1 до 50 символов")]
        public string Name { get; set; }
        public byte[] ImageData { get; set; }

        [Display(Name = "Лого")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [Display(Name = "Удалить текущее лого?")]
        public bool DeletePhoto { get; set; }
    }
}
