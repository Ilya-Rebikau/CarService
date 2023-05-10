using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    public class Feedback : IModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
