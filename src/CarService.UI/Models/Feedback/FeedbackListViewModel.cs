namespace CarService.UI.Models.Feedback
{
    public class FeedbackListViewModel
    {
        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
        public string UserId { get; set; }
        public FeedbackViewModel NewFeedback { get; set; }
    }
}
