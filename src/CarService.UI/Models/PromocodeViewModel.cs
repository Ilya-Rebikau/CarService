using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models
{
    public class PromocodeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Размер скидки, %")]
        [Range(1, 100, ErrorMessage = "Размер должен быть от 1 до 100")]
        public int Percent { get; set; }

        [Display(Name = "Срок действия")]
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Промокод")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Длина должна быть 10 символов")]
        public string Text { get; set; }
    }
}
