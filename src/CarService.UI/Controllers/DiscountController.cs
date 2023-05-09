﻿using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly ICarTypeService _carTypeService;
        private readonly ICarBrandService _carBrandService;
        private readonly IServiceDataService _serviceDataService;
        public DiscountController(IDiscountService discountService,
            ICarBrandService carBrandService,
            ICarTypeService carTypeService,
            IServiceDataService serviceDataService)
        {
            _discountService = discountService;
            _carTypeService = carTypeService;
            _carBrandService = carBrandService;
            _serviceDataService = serviceDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var discounts = await _discountService.GetDiscounts(HttpContext.GetJwt());
            return View(discounts);
        }

        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var carBrands = await _carBrandService.GetCarBrandViewModels(HttpContext.GetJwt());
            var carTypes = await _carTypeService.GetAll(HttpContext.GetJwt());
            var serviceDatas = await _serviceDataService.GetServiceDatas(HttpContext.GetJwt());
            CarBrandViewModel tempCarBrand;
            CarType tempCarType;
            ServiceDataViewModel tempServiceData;
            var discountViewModel = new DiscountViewModel
            {
                CarBrandSelectList = new SelectList(carBrands, nameof(tempCarBrand.Id), nameof(tempCarBrand.Name)),
                CarTypeSelectList = new SelectList(carTypes, nameof(tempCarType.Id), nameof(tempCarType.Name)),
                ServiceDataSelectList = new SelectList(serviceDatas, nameof(tempServiceData.Id), nameof(tempServiceData.Name))
            };

            return View(discountViewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiscountViewModel discount)
        {
            if (!ModelState.IsValid)
            {
                return View(discount);
            }

            await _discountService.CreateDiscount(HttpContext.GetJwt(), discount);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var discount = await _discountService.GetDiscountById(HttpContext.GetJwt(), (int)id);
            var carBrands = await _carBrandService.GetCarBrandViewModels(HttpContext.GetJwt());
            var carTypes = await _carTypeService.GetAll(HttpContext.GetJwt());
            var serviceDatas = await _serviceDataService.GetServiceDatas(HttpContext.GetJwt());
            CarBrandViewModel tempCarBrand;
            CarType tempCarType;
            ServiceDataViewModel tempServiceData;
            discount.CarBrandSelectList = new SelectList(carBrands, nameof(tempCarBrand.Id), nameof(tempCarBrand.Name));
            discount.CarTypeSelectList = new SelectList(carTypes, nameof(tempCarType.Id), nameof(tempCarType.Name));
            discount.ServiceDataSelectList = new SelectList(serviceDatas, nameof(tempServiceData.Id), nameof(tempServiceData.Name));
            return id is null ? NotFound() : View(discount);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DiscountViewModel discount)
        {
            if (id != discount.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(discount);
            }

            try
            {
                await _discountService.EditDiscount(HttpContext.GetJwt(), id, discount);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }

            return RedirectToAction(nameof(Index), new { serviceDataId = discount.ServiceDataId });
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int serviceDataId)
        {
            await _discountService.DeleteDiscount(HttpContext.GetJwt(), id);
            return RedirectToAction(nameof(Index), new { serviceDataId });
        }
    }
}
