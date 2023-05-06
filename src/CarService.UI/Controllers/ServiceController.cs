using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    [ExceptionFilter]
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;
        private readonly ICarBrandService _carBrandService;
        private readonly ICarTypeService _carTypeService;
        private readonly IServiceDataService _serviceDataService;
        public ServiceController(IServiceService service,
            ICarBrandService carBrandService,
            ICarTypeService carTypeService,
            IServiceDataService serviceDataService)
        {
            _service = service;
            _carBrandService = carBrandService;
            _carTypeService = carTypeService;
            _serviceDataService = serviceDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int serviceDataId)
        {
            var services = await _service.GetAllByServiceDataId(HttpContext.GetJwt(), serviceDataId);
            var serviceData = await _serviceDataService.GetServiceDataById(HttpContext.GetJwt(), serviceDataId);
            var serviceListViewModel = new ServiceListViewModel
            {
                ServiceDataId = serviceDataId,
                Services = services,
                ServiceName = serviceData.Name,
                ServiceImageData = serviceData.ImageData
            };
            return View(serviceListViewModel);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public async Task<IActionResult> Create(int serviceDataId)
        {
            var carBrands = await _carBrandService.GetCarBrandViewModels(HttpContext.GetJwt());
            var carTypes = await _carTypeService.GetAll(HttpContext.GetJwt());
            CarBrandViewModel tempCarBrand;
            CarType tempCarType;
            var serviceViewModel = new ServiceViewModel
            {
                ServiceDataId = serviceDataId,
                CarBrandSelectList = new SelectList(carBrands, nameof(tempCarBrand.Id), nameof(tempCarBrand.Name)),
                CarTypeSelectList = new SelectList(carTypes, nameof(tempCarType.Id), nameof(tempCarType.Name))
            };

            return View(serviceViewModel);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceViewModel service)
        {
            if (!ModelState.IsValid)
            {
                return View(service);
            }

            await _service.CreateService(HttpContext.GetJwt(), service);
            return RedirectToAction(nameof(Index), new { serviceDataId = service.ServiceDataId });
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var service = await _service.GetServiceById(HttpContext.GetJwt(), (int)id);
            var carBrands = await _carBrandService.GetCarBrandViewModels(HttpContext.GetJwt());
            var carTypes = await _carTypeService.GetAll(HttpContext.GetJwt());
            CarBrandViewModel tempCarBrand;
            CarType tempCarType;
            service.CarBrandSelectList = new SelectList(carBrands, nameof(tempCarBrand.Id), nameof(tempCarBrand.Name));
            service.CarTypeSelectList = new SelectList(carTypes, nameof(tempCarType.Id), nameof(tempCarType.Name));
            return id is null ? NotFound() : View(service);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceViewModel service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(service);
            }

            try
            {
                await _service.EditService(HttpContext.GetJwt(), id, service);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }

            return RedirectToAction(nameof(Index), new { serviceDataId = service.ServiceDataId });
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int serviceDataId)
        {
            await _service.DeleteService(HttpContext.GetJwt(), id);
            return RedirectToAction(nameof(Index), new { serviceDataId });
        }
    }
}
