using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.UI.Controllers
{
    public class ServiceDataController : Controller
    {
        private readonly IServiceDataService _service;
        public ServiceDataController(IServiceDataService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var serviceDatas = await _service.GetServiceDatas(HttpContext.GetJwt(), pageNumber);
            var nextServiceDatas = await _service.GetServiceDatas(HttpContext.GetJwt(), pageNumber + 1);
            PageViewModel.NextPage = nextServiceDatas is not null && nextServiceDatas.Any();
            PageViewModel.PageNumber = pageNumber;
            return View(serviceDatas);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceDataViewModel serviceData)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceData);
            }

            await _service.CreateServiceData(HttpContext.GetJwt(), serviceData);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return id is null ? NotFound() : View(await _service.GetServiceDataById(HttpContext.GetJwt(), (int)id));
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceDataViewModel serviceData)
        {
            if (id != serviceData.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(serviceData);
            }

            try
            {
                await _service.EditServiceData(HttpContext.GetJwt(), id, serviceData);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteServiceData(HttpContext.GetJwt(), id);
            return RedirectToAction(nameof(Index));
        }
    }
}
