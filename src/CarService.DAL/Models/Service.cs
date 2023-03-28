namespace CarService.DAL.Models
{
    /// <summary>
    /// Услуга.
    /// </summary>
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public TimeOnly TimeSpent { get; set; }
    }
}
