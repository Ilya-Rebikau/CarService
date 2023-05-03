using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.UI.Controllers
{
    [Authorize(Roles = "admin")]
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
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var services = await _service.GetServiceViewModels(HttpContext.GetJwtToken(), pageNumber);
            var nextServices = await _service.GetServiceViewModels(HttpContext.GetJwtToken(), pageNumber + 1);
            PageModel.NextPage = nextServices is not null && nextServices.Any();
            PageModel.PageNumber = pageNumber;
            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return id is null ? NotFound() : View(await _service.ServiceDetails(HttpContext.GetJwtToken(), (int)id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return id is null ? NotFound() : View(await _service.GetServiceViewModelForEdit(HttpContext.GetJwtToken(), (int)id));
        }

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

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return id is null ? NotFound() : View(await _service.GetServiceViewModelForDelete(HttpContext.GetJwtToken(), (int)id));
        }

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
