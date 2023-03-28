namespace CarService.DAL.Models
{
    /// <summary>
    /// Скидка.
    /// </summary>
    public class Discount
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public int ServiceId { get; set; }
    }
}
