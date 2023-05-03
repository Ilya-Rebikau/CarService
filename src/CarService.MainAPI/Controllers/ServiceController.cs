using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.MainAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;
        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet("getservices")]
        public IActionResult GetServices([FromQuery] int pageNumber)
        {
            var services = _service.GetAll(pageNumber);
            return Ok(services);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var service = await _service.GetById(id);
            return service is null ? NotFound() : Ok(service);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Service service)
        {
            await _service.Create(service);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _service.GetById(id);
            return service is null ? NotFound() : Ok(service);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            try
            {
                await _service.Update(service);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ServiceExists(service.Id))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict();
                }
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var service = await _service.GetById(id);
            return service is null ? NotFound() : Ok(service);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
        {
            await _service.DeleteById(id);
            return Ok();
        }

        private async Task<bool> ServiceExists(int id)
        {
            return await _service.GetById(id) is not null;
        }
    }
}
