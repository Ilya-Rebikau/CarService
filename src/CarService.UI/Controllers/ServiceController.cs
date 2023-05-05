using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    [ExceptionFilter]
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;
        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int carTypeId = 0, int carBrandId = 0)
        {
            var serviceListModel = await _service.GetServiceListViewModel(HttpContext.GetJwtToken(), carTypeId, carBrandId);
            return View(serviceListModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return id is null ? NotFound() : View(await _service.GetServiceViewModelById(HttpContext.GetJwtToken(), (int)id));
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var serviceViewModel = await _service.GetServiceViewModelForCreate(HttpContext.GetJwtToken());
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

            await _service.CreateService(HttpContext.GetJwtToken(), service);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return id is null ? NotFound() : View(await _service.GetServiceViewModelById(HttpContext.GetJwtToken(), (int)id));
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
                await _service.EditService(HttpContext.GetJwtToken(), id, service);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return id is null ? NotFound() : View(await _service.GetServiceViewModelById(HttpContext.GetJwtToken(), (int)id));
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteService(HttpContext.GetJwtToken(), id);
            return RedirectToAction(nameof(Index));
        }
    }
}
