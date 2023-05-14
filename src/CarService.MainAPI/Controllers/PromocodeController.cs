using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.MainAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PromocodeController : ControllerBase
    {
        private readonly IPromocodeService _promocodeService;
        public PromocodeController(IPromocodeService promocodeService)
        {
            _promocodeService = promocodeService;
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpGet("getpromocodes")]
        public IActionResult GetPromocodesByUser([FromQuery] string userId)
        {
            var promocodes = _promocodeService.GetAllByUser(userId);
            return Ok(promocodes);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost("usepromocode")]
        public async Task<IActionResult> UsePromocode([FromQuery] string text)
        {
            var promocode = _promocodeService.GetPromocodeByText(text);
            if (promocode is null || promocode.DateEnd.Date < DateTime.Now.Date)
            {
                return NotFound();
            }

            await _promocodeService.DeleteById(promocode.Id);
            return Ok(promocode);
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Promocode promocode)
        {
            await _promocodeService.Create(promocode);
            return Ok();
        }
    }
}
