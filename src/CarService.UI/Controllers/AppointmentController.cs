using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models;
using CarService.UI.Models.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CarService.UI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAccountService _accountService;
        public AppointmentController(IAppointmentService appointmentService, IAccountService accountService)
        {
            _appointmentService = appointmentService;
            _accountService = accountService;
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1)
        {
            var appointments = await _appointmentService.GetAllAppointments(HttpContext.GetJwt(), pageNumber);
            var nextAppointments = await _appointmentService.GetAllAppointments(HttpContext.GetJwt(), pageNumber + 1);
            PageViewModel.NextPage = nextAppointments is not null && nextAppointments.Any();
            PageViewModel.PageNumber = pageNumber;
            return View(appointments);
        }

        [Authorize(Roles = "admin, manager, user")]
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

        [Authorize(Roles = "admin, manager, user")]
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

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public async Task<IActionResult> Finish(int id)
        {
            await _appointmentService.FinishAppointment(HttpContext.GetJwt(), id);
            return RedirectToAction(nameof(GetAll));
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentService.DeleteAppointment(HttpContext.GetJwt(), id);
            return RedirectToAction(nameof(GetAll));
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost]
        public async Task<IActionResult> DeleteForUser(int id)
        {
            await _appointmentService.DeleteAppointment(HttpContext.GetJwt(), id);
            return RedirectToAction(nameof(Index), typeof(AccountController).GetControllerName());
        }
    }
}
