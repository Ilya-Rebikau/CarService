using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    public class ServiceData : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public string Description { get; set; }
    }
}
