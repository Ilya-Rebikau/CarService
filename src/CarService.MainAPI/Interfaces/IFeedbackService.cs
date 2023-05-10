using CarService.DAL.Models;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Interfaces
{
    public interface IFeedbackService : IBaseService<Feedback>
    {
        public Task<IEnumerable<FeedbackModel>> GetAllFeedbacks(string token, int pageNumber);
        public Task CreateFeedbackModel(FeedbackModel feedbackModel);
    }
}
