﻿using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.CarBrands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    [ExceptionFilter]
    public class CarBrandController : Controller
    {
        private readonly ICarBrandService _service;
        public CarBrandController(ICarBrandService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var carBrands = await _service.GetCarBrandViewModels(HttpContext.GetJwtToken(), pageNumber);
            var nextCarBrands = await _service.GetCarBrandViewModels(HttpContext.GetJwtToken(), pageNumber + 1);
            PageModel.NextPage = nextCarBrands is not null && nextCarBrands.Any();
            PageModel.PageNumber = pageNumber;
            return View(carBrands);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarBrandViewModel carBrand)
        {
            if (!ModelState.IsValid)
            {
                return View(carBrand);
            }

            await _service.CreateCarBrand(HttpContext.GetJwtToken(), carBrand);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return id is null ? NotFound() : View(await _service.GetCarBrandViewModelForEdit(HttpContext.GetJwtToken(), (int)id));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarBrandViewModel carBrand)
        {
            if (id != carBrand.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(carBrand);
            }

            try
            {
                await _service.EditCarBrand(HttpContext.GetJwtToken(), id, carBrand);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCarBrand(HttpContext.GetJwtToken(), id);
            return RedirectToAction(nameof(Index));
        }
    }
}