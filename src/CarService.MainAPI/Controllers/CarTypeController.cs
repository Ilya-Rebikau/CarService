using CarService.MainAPI.Infrastructure;
using CarService.MainAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.MainAPI.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
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
    }
}
