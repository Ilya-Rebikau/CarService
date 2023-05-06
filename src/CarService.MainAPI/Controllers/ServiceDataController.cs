using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.MainAPI.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class ServiceDataController : ControllerBase
    {
        private readonly IServiceDataService _service;
        public ServiceDataController(IServiceDataService service)
        {
            _service = service;
        }

        [HttpGet("getservicedatas")]
        public IActionResult GetServiceDatas([FromQuery] int pageNumber)
        {
            var serviceDatas = _service.GetAll(pageNumber);
            return Ok(serviceDatas);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var service = await _service.GetById(id);
            return service is null ? NotFound() : Ok(service);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ServiceData serviceData)
        {
            await _service.Create(serviceData);
            return Ok();
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ServiceData serviceData)
        {
            if (id != serviceData.Id)
            {
                return NotFound();
            }

            try
            {
                await _service.Update(serviceData);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ServiceDataExists(serviceData.Id))
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

        private async Task<bool> ServiceDataExists(int id)
        {
            return await _service.GetById(id) is not null;
        }
    }
}
