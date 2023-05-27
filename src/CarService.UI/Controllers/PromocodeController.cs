using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.UI.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class PromocodeController : Controller
    {
        private readonly IPromocodeService _promocodeService;
        public PromocodeController(IPromocodeService promocodeService)
        {
            _promocodeService = promocodeService;
        }

        [HttpGet]
        public IActionResult UsePromocode()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UsePromocode(PromocodeViewModel promocode)
        {
            promocode = await _promocodeService.UsePromocode(HttpContext.GetJwt(), promocode.Text);
            return RedirectToAction(nameof(UsePromocodeResult), new { percent = promocode.Percent });
        }

        [HttpGet]
        public IActionResult UsePromocodeResult(int percent)
        {
            return View(percent);
        }

        [HttpGet]
        public IActionResult Create(string userId)
        {
            var promocode = new PromocodeViewModel
            {
                UserId = userId
            };
            return View(promocode);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PromocodeViewModel promocode)
        {
            await _promocodeService.Create(HttpContext.GetJwt(), promocode);
            return RedirectToAction(nameof(Index), typeof(UserController).GetControllerName());
        }
    }
}
