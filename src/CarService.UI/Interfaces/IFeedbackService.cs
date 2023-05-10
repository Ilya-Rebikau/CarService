using CarService.UI.Models.Feedback;

namespace CarService.UI.Interfaces
{
    public interface IFeedbackService
    {
        Task<FeedbackListViewModel> GetFeedbacks(string token, int pageNumber);
        Task CreateFeedback(string token,  FeedbackViewModel feedback);
        Task DeleteFeedback(string token, int id);
    }
}
