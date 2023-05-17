using AutoMapper;
using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;
using CarService.MainAPI.Interfaces.HttpClients;
using CarService.MainAPI.Models;

namespace CarService.MainAPI.Services
{
    internal class FeedbackService : BaseService<Feedback>, IFeedbackService
    {
        private readonly IMapper _mapper;
        private readonly IUserClient _userClient;
        public FeedbackService(IRepository<Feedback> repository, IConfiguration configuration,
            IMapper mapper, IUserClient userClient)
            : base(repository, configuration)
        {
            _mapper = mapper;
            _userClient = userClient;
        }

        public async Task<IEnumerable<FeedbackModel>> GetAllFeedbacks(string token, int pageNumber)
        {
            var feedbacks = Repository.GetAll().OrderByDescending(f => f.DateTime)
                .Skip((pageNumber - 1) * CountOnPage).Take(CountOnPage);
            var feedbackModels = new List<FeedbackModel>();
            foreach (var feedback in feedbacks)
            {
                var feedbackModel = _mapper.Map<FeedbackModel>(feedback);
                var userName = await _userClient.GetUserName(token, feedback.UserId);
                if (string.IsNullOrWhiteSpace(userName))
                {
                    userName = "Без имени";
                }

                feedbackModel.UserName = userName;
                feedbackModel.PhotoData = await _userClient.GetUserPhotoData(token, feedback.UserId);
                feedbackModels.Add(feedbackModel);
            }

            return feedbackModels;
        }

        public async Task CreateFeedbackModel(FeedbackModel feedbackModel)
        {
            var feedback = _mapper.Map<Feedback>(feedbackModel);
            await Create(feedback);
        }
    }
}
