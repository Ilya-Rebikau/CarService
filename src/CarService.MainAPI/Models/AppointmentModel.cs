namespace CarService.MainAPI.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public bool WasFinished { get; set; }
        public int ServiceId { get; set; }
        public string ServiceData { get; set; }
        public string UserEmail { get; set; }
    }
}
