using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    [ExceptionFilter]
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

        [Authorize(Roles = "admin, manager")]
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

        [Authorize(Roles = "admin, manager")]
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
    }
}
