using CarService.DAL.Models;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface IFeedbackService : IBaseService<Feedback>
    {
        Task<IEnumerable<FeedbackModel>> GetAllFeedbacks(string token, int pageNumber);
        Task CreateFeedbackModel(FeedbackModel feedbackModel);
    }
}
