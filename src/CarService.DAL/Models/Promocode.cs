using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    public class Promocode : IModel
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        public DateTime DateEnd { get; set; }
        public bool WasUsed { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
    }
}
