﻿using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.Appointment;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAccountService _accountService;
        public AppointmentController(IAppointmentService appointmentService, IAccountService accountService)
        {
            _appointmentService = appointmentService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int serviceId, string dateTime)
        {
            var newDateTime = DateTime.Now.ToString("dd-MM-yyyy");
            if (!string.IsNullOrWhiteSpace(dateTime))
            {
                newDateTime = dateTime;
            }

            var appointments = await _appointmentService.GetAppointmentsByDateAndService(HttpContext.GetJwt(), newDateTime, serviceId);
            var appointmentListViewModel = new AppointmentListViewModel
            {
                Appointments = appointments,
                Date = DateTime.ParseExact(newDateTime, "dd-MM-yyyy", CultureInfo.GetCultureInfo("ru-RU")),
                NewAppointment = new AppointmentViewModel
                {
                    ServiceId = serviceId,
                    WasFinished = false
                }
            };
            return View(appointmentListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentsByUser(string userId, int pageNumber = 1)
        {
            var appointments = await _appointmentService.GetAppointmentsByUser(HttpContext.GetJwt(), userId, pageNumber);
            var nextAppointments = await _appointmentService.GetAppointmentsByUser(HttpContext.GetJwt(), userId, pageNumber + 1);
            PageModel.NextPage = nextAppointments is not null && nextAppointments.Any();
            PageModel.PageNumber = pageNumber;
            return View(appointments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentListViewModel appointmentList)
        {
            if (!ModelState.IsValid)
            {
                return View(appointmentList);
            }

            var currentUser = await _accountService.GetAccountViewModel(HttpContext);
            appointmentList.NewAppointment.UserId = currentUser.Id;
            await _appointmentService.CreateAppointment(HttpContext.GetJwt(), appointmentList.NewAppointment);
            return RedirectToAction(nameof(Index), new 
            {
                serviceId = appointmentList.NewAppointment.ServiceId,
                dateTime = appointmentList.NewAppointment.DateTimeStart.ToString("dd-MM-yyyy")
            });
        }
    }
}
