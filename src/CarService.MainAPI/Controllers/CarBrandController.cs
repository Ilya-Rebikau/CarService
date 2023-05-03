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
    public class CarBrandController : ControllerBase
    {
        private readonly ICarBrandService _carBrandService;
        public CarBrandController(ICarBrandService carBrandService)
        {
            _carBrandService = carBrandService;
        }

        [HttpGet("getcarbrands")]
        public IActionResult GetCarBrands([FromQuery] int pageNumber)
        {
            var carBrands = _carBrandService.GetAll(pageNumber);
            return Ok(carBrands);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CarBrand carBrand)
        {
            await _carBrandService.Create(carBrand);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var carBrand = await _carBrandService.GetById(id);
            return carBrand is null ? NotFound() : Ok(carBrand);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CarBrand carBrand)
        {
            if (id != carBrand.Id)
            {
                return NotFound();
            }

            try
            {
                await _carBrandService.Update(carBrand);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CarBrandExists(carBrand.Id))
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
            await _carBrandService.DeleteById(id);
            return Ok();
        }

        private async Task<bool> CarBrandExists(int id)
        {
            return await _carBrandService.GetById(id) is not null;
        }
    }
}
