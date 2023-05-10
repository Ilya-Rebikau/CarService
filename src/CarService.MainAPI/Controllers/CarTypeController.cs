using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.MainAPI.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class CarTypeController : ControllerBase
    {
        private readonly ICarTypeService _carTypeService;
        public CarTypeController(ICarTypeService carTypeService)
        {
            _carTypeService = carTypeService;
        }

        [HttpGet("getcartypes")]
        public IActionResult GetCarTypes()
        {
            var carTypes = _carTypeService.GetAll();
            return Ok(carTypes);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CarType carType)
        {
            await _carTypeService.Create(carType);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var carType = await _carTypeService.GetById(id);
            return carType is null ? NotFound() : Ok(carType);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CarType carType)
        {
            if (id != carType.Id)
            {
                return NotFound();
            }

            try
            {
                await _carTypeService.Update(carType);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CarTypeExists(carType.Id))
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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _carTypeService.DeleteById(id);
            return Ok();
        }

        private async Task<bool> CarTypeExists(int id)
        {
            return await _carTypeService.GetById(id) is not null;
        }
    }
}
