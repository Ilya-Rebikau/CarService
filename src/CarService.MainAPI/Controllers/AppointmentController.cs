using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CarService.MainAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private const string AuthorizationKey = "Authorization";
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("getallappointments")]
        public async Task<IActionResult> GetAllAppointments([FromHeader(Name = AuthorizationKey)] string token, [FromQuery] int pageNumber)
        {
            var appointments = await _appointmentService.GetAllAppointments(token, pageNumber);
            return Ok(appointments);
        }

        [HttpGet("getappointmentsbydateandservice")]
        public async Task<IActionResult> GetAppointmentsByDateAndService([FromQuery] string stringDateTime, [FromQuery] int serviceId)
        {
            var dateTime = DateTime.ParseExact(stringDateTime, "dd-MM-yyyy", CultureInfo.GetCultureInfo("ru-RU"));
            var appointments = await _appointmentService.GetAllByDateAndServiceData(dateTime, serviceId);
            return Ok(appointments);
        }

        [HttpGet("getappointmentsbyuser")]
        public async Task<IActionResult> GetAppointmentsByUser([FromQuery] string userId)
        {
            var appointments = await _appointmentService.GetAllByUserId(userId);
            return Ok(appointments);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Appointment appointment)
        {
            await _appointmentService.Create(appointment);
            return Ok();
        }

        [HttpPut("finish/{id}")]
        public async Task<IActionResult> Finish([FromRoute] int id)
        {
            try
            {
                await _appointmentService.FinishAppointment(id);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict();
                }
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _appointmentService.DeleteById(id);
            return Ok();
        }

        private async Task<bool> AppointmentExists(int id)
        {
            return await _appointmentService.GetById(id) is not null;
        }
    }
}
