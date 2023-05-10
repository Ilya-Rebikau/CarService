using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Service
{
    public class ServiceDataViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название услуги")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Длина должна быть от 1 до 100 символов")]
        public string Name { get; set; }
        public byte[] ImageData { get; set; }

        [Display(Name = "Картинка")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [Display(Name = "Удалить текущую картинку?")]
        public bool DeletePhoto { get; set; }
    }
}
