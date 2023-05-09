using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Service
{
    public class ServiceDataViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название услуги")]
        public string Name { get; set; }
        public byte[] ImageData { get; set; }

        [Display(Name = "Картинка")]
        public IFormFile Image { get; set; }

        [Display(Name = "Удалить текущую картинку?")]
        public bool DeletePhoto { get; set; }
    }
}
