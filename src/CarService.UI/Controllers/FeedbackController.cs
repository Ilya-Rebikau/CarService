using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.Feedback;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var feedbacks = await _feedbackService.GetFeedbacks(HttpContext.GetJwt(), pageNumber);
            feedbacks.NewFeedback = new FeedbackViewModel
            {
                UserId = feedbacks.UserId,
                DateTime = DateTime.Now,
            };
            var nextFeedbacks = await _feedbackService.GetFeedbacks(HttpContext.GetJwt(), pageNumber + 1);
            PageViewModel.NextPage = nextFeedbacks.Feedbacks is not null && nextFeedbacks.Feedbacks.Any();
            PageViewModel.PageNumber = pageNumber;
            return View(feedbacks);
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeedbackListViewModel feedbackList)
        {
            if (!ModelState.IsValid)
            {
                return View(feedbackList.NewFeedback);
            }

            await _feedbackService.CreateFeedback(HttpContext.GetJwt(), feedbackList.NewFeedback);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _feedbackService.DeleteFeedback(HttpContext.GetJwt(), id);
            return RedirectToAction(nameof(Index));
        }
    }
}
