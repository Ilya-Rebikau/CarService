using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class CarTypeController : Controller
    {
        private readonly ICarTypeService _carTypeService;
        public CarTypeController(ICarTypeService carTypeService)
        {
            _carTypeService = carTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var carTypes = await _carTypeService.GetAll(HttpContext.GetJwt());
            return View(carTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarTypeViewModel carType)
        {
            if (!ModelState.IsValid)
            {
                return View(carType);
            }

            await _carTypeService.CreateCarType(HttpContext.GetJwt(), carType);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return id is null ? NotFound() : View(await _carTypeService.GetCarTypeViewModelForEdit(HttpContext.GetJwt(), (int)id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarTypeViewModel carType)
        {
            if (id != carType.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(carType);
            }

            try
            {
                await _carTypeService.EditCarType(HttpContext.GetJwt(), id, carType);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _carTypeService.DeleteCarType(HttpContext.GetJwt(), id);
            return RedirectToAction(nameof(Index));
        }
    }
}
