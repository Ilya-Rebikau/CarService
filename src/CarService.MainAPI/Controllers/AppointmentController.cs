using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CarService.MainAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("getappointmentsbydateandservice")]
        public async Task<IActionResult> GetAppointmentsByDateAndService([FromQuery] string stringDateTime, [FromQuery] int serviceId)
        {
            var dateTime = DateTime.ParseExact(stringDateTime, "dd-MM-yyyy", CultureInfo.GetCultureInfo("ru-RU"));
            var appointments = await _appointmentService.GetAllByDateAndServiceData(dateTime, serviceId);
            return Ok(appointments);
        }

        [HttpGet("getappointmentsbyuser")]
        public IActionResult GetAppointmentsByUser([FromQuery] string userId, [FromQuery] int pageNumber)
        {
            var appointments = _appointmentService.GetAllByUserId(userId, pageNumber);
            return Ok(appointments);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Appointment appointment)
        {
            await _appointmentService.Create(appointment);
            return Ok();
        }
    }
}
