﻿using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;
using CarService.MainAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.MainAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private const string AuthorizationKey = "Authorization";
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [AllowAnonymous]
        [HttpGet("getfeedbacks")]
        public async Task<IActionResult> GetFeedbacks([FromHeader(Name = AuthorizationKey)] string token, [FromQuery] int pageNumber)
        {
            var feedbacks = await _feedbackService.GetAllFeedbacks(token, pageNumber);
            return Ok(feedbacks);
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] FeedbackModel feedback)
        {
            await _feedbackService.CreateFeedbackModel(feedback);
            return Ok();
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _feedbackService.DeleteById(id);
            return Ok();
        }
    }
}
