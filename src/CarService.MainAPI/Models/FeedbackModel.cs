namespace CarService.MainAPI.Models
{
    public class FeedbackModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public byte[] PhotoData { get; set; }
        public DateTime DateTime { get; set; }
    }
}
