using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models
{
    public class PromocodeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Размер скидки, %")]
        public int Percent { get; set; }

        [Display(Name = "Срок действия")]
        public DateTime DateEnd { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Промокод")]
        public string Text { get; set; }
    }
}
