using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Feedback
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Отзыв")]
        [Required(ErrorMessage = "Отзыв обязателен")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [Display(Name = "Имя")]
        public string UserName { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Фото")]
        public byte[] PhotoData { get; set; }

        [Display(Name = "Дата")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
    }
}
