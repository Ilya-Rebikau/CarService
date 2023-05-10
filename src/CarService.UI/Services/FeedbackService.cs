using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models.Feedback;

namespace CarService.UI.Services
{
    internal class FeedbackService : IFeedbackService
    {
        private readonly IMainClient _mainClient;
        private readonly IUserClient _userClient;
        public FeedbackService(IMainClient mainClient, IUserClient userClient)
        {
            _mainClient = mainClient;
            _userClient = userClient;
        }

        public async Task CreateFeedback(string token, FeedbackViewModel feedback)
        {
            await _mainClient.CreateFeedback(token, feedback);
        }

        public async Task DeleteFeedback(string token, int id)
        {
            await _mainClient.DeleteFeedback(token, id);
        }

        public async Task<FeedbackListViewModel> GetFeedbacks(string token, int pageNumber)
        {
            var feedbacks = await _mainClient.GetFeedbacks(token, pageNumber);
            var account = await _userClient.GetAccountViewModel(token);
            var feedbackListViewModel = new FeedbackListViewModel
            {
                Feedbacks = feedbacks,
                UserId = account.Id
            };

            return feedbackListViewModel;
        }
    }
}
