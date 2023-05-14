using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly IPromocodeService _promocodeService;
        private readonly IAppointmentService _appointmentService;
        public AccountController(IAccountService service, IPromocodeService promocodeService,
            IAppointmentService appointmentService)
        {
            _service = service;
            _promocodeService = promocodeService;
            _appointmentService = appointmentService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var registerResult = await _service.RegisterUser(model, HttpContext);
            if (registerResult.IdentityResult.Errors.Any())
            {
                foreach (var error in registerResult.IdentityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            await _service.LoginUser(model, HttpContext);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _service.LoginUser(model, HttpContext);
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return RedirectToAction(nameof(Index), typeof(HomeController).GetControllerName());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager, user")]
        public async Task<IActionResult> Logout()
        {
            await _service.Logout(HttpContext);
            return RedirectToAction(nameof(Index), typeof(HomeController).GetControllerName());
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var editAccountViewModel = await _service.GetEditAccountViewModelForEdit(HttpContext, id);
            return editAccountViewModel is null ? NotFound() : View(editAccountViewModel);
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.UpdateUserInEdit(HttpContext, model);
            if (!result.Errors.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accountVm = await _service.GetAccountViewModel(HttpContext);
            var appointments = await _appointmentService.GetAppointmentsByUser(HttpContext.GetJwt(), accountVm.Id);
            var promocodes = await _promocodeService.GetAllByUser(HttpContext.GetJwt(), accountVm.Id);
            accountVm.Promocodes = promocodes;
            accountVm.Appointments = appointments;
            return View(accountVm);
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            var model = new ChangePasswordInPersonalAccountViewModel { Id = id };
            return View(model);
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordInPersonalAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.ChangePassword(HttpContext, model);
            if (!result.Errors.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}