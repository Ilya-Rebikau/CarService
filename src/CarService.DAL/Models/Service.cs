using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    /// <summary>
    /// Услуга.
    /// </summary>
    public class Service : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public TimeOnly TimeSpent { get; set; }
    }
}
