using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    [ExceptionFilter]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _service.RegisterUser(model, HttpContext);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
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
            var accountViewModel = await _service.GetEditAccountViewModelForEdit(HttpContext, id);
            return accountViewModel is null ? NotFound() : View(accountViewModel);
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
            return View(accountVm);
        }
    }
}