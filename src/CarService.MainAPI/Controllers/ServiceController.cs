using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;
using CarService.MainAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.MainAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;
        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("getservices")]
        public async Task<IActionResult> GetServices([FromQuery] int serviceDataId)
        {
            var services = await _service.GetAllByServiceDataId(serviceDataId);
            return Ok(services);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var service = await _service.GetServiceModelById(id);
            return service is null ? NotFound() : Ok(service);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ServiceModel service)
        {
            await _service.CreateServiceModel(service);
            return Ok();
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ServiceModel service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateServiceModel(service);
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

        [Authorize(Roles = "admin, manager")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
