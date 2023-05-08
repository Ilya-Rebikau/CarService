using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    [ExceptionFilter]
    public class PromocodeController : Controller
    {
        private readonly IPromocodeService _promocodeService;
        public PromocodeController(IPromocodeService promocodeService)
        {
            _promocodeService = promocodeService;
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public IActionResult UsePromocode()
        {
            return View();
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public async Task<IActionResult> UsePromocode(PromocodeViewModel promocode)
        {
            await _promocodeService.UsePromocode(HttpContext.GetJwt(), promocode.Text);
            return RedirectToAction(nameof(Index), typeof(HomeController).GetControllerName());
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpGet]
        public IActionResult Create(string userId)
        {
            var promocode = new PromocodeViewModel
            {
                UserId = userId
            };
            return View(promocode);
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost]
        public async Task<IActionResult> Create(PromocodeViewModel promocode)
        {
            await _promocodeService.Create(HttpContext.GetJwt(), promocode);
            return RedirectToAction(nameof(Index), typeof(UserController).GetControllerName());
        }
    }
}
