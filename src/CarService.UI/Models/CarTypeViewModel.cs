using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models
{
    public class CarTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Тип техники")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина должна быть от 1 до 50 символов")]
        public string Name { get; set; }
    }
}
