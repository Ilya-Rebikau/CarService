using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var users = await _service.GetUsers(HttpContext.GetJwt(), pageNumber);
            var nextUsers = await _service.GetUsers(HttpContext.GetJwt(), pageNumber + 1);
            PageViewModel.NextPage = nextUsers is not null && nextUsers.Any();
            PageViewModel.PageNumber = pageNumber;
            return View(users);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.CreateUser(HttpContext.GetJwt(), model);
            if (!result.Errors.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View(await _service.GetEditUserViewModel(HttpContext.GetJwt(), id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.EditUser(HttpContext.GetJwt(), model);
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

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteUser(HttpContext.GetJwt(), id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            return View(await _service.GetChangePasswordViewModel(HttpContext.GetJwt(), id));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.ChangePassowrd(HttpContext.GetJwt(), model);
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

        [HttpGet]
        public async Task<IActionResult> EditRoles(string userId)
        {
            return View(await _service.GetChangeRoleViewModel(HttpContext.GetJwt(), userId));
        }

        [HttpPost]
        public async Task<IActionResult> EditRoles(ChangeRoleViewModel model)
        {
            await _service.ChangeRoles(HttpContext.GetJwt(), model);
            return RedirectToAction(nameof(Index));
        }
    }
}