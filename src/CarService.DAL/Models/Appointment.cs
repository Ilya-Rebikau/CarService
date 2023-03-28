using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    /// <summary>
    /// Запись на прохождение технического обслуживания.
    /// </summary>
    public class Appointment : IModel
    {
        public int Id { get; set; }
        public int CarTypeId { get; set; }
        public int CarBrandId { get; set; }
        public float CarMileage { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
