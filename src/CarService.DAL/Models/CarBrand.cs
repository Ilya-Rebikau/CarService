using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    /// <summary>
    /// Марка автомобиля.
    /// </summary>
    public class CarBrand : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
    }
}
