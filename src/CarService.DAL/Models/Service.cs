using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    /// <summary>
    /// Услуга.
    /// </summary>
    public class Service : IModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int MinutesSpent { get; set; }
        public int? CarBrandId { get; set; }
        public int? CarTypeId { get; set; }
        public int ServiceDataId { get; set; }
    }
}
