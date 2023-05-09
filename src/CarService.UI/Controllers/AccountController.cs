﻿using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public AccountController(IAccountService service, IPromocodeService promocodeService)
        {
            _service = service;
            _promocodeService = promocodeService;
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
        
        public async Task<IActionResult> Logout()
        {
            await _service.Logout(HttpContext);
            return RedirectToAction(nameof(Index), typeof(HomeController).GetControllerName());
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var editAccountViewModel = await _service.GetEditAccountViewModelForEdit(HttpContext, id);
            return editAccountViewModel is null ? NotFound() : View(editAccountViewModel);
        }

        
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

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accountVm = await _service.GetAccountViewModel(HttpContext);
            var promocodes = await _promocodeService.GetAllByUser(HttpContext.GetJwt(), accountVm.Id);
            accountVm.Promocodes = promocodes.ToList();
            return View(accountVm);
        }

        
        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            var model = new ChangePasswordInPersonalAccountViewModel { Id = id };
            return View(model);
        }

        
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