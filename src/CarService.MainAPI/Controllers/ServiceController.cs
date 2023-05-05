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
        public IActionResult GetServices([FromQuery] int carTypeId, [FromQuery] int carBrandId)
        {
            var services = _service.GetServiceListModel(carTypeId, carBrandId);
            return Ok(services);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var service = await _service.GetServiceModelById(id);
            return service is null ? NotFound() : Ok(service);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet("create")]
        public IActionResult Create()
        {
            var serviceModel = _service.GetServiceModelForCreate();
            return Ok(serviceModel);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Service service)
        {
            await _service.Create(service);
            return Ok();
        }

        [Authorize(Roles = "admin, manager")]
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
