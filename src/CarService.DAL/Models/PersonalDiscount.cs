using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    public class PersonalDiscount : IModel
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int ServiceId { get; set; }
        public string UserId { get; set; }
    }
}
