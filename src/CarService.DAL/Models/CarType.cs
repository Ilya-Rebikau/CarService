using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    /// <summary>
    /// Тип автомобиля.
    /// </summary>
    public class CarType : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
