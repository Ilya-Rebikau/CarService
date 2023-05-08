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
    [ExceptionFilter]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("getdiscounts")]
        public async Task<IActionResult> GetDiscounts()
        {
            var discounts = await _discountService.GetAllDiscountModels();
            return Ok(discounts);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var discount = await _discountService.GetDiscountModelById(id);
            return discount is null ? NotFound() : Ok(discount);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DiscountModel discount)
        {
            await _discountService.CreateDiscountModel(discount);
            return Ok();
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var discount = await _discountService.GetDiscountModelById(id);
            return discount is null ? NotFound() : Ok(discount);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] DiscountModel discount)
        {
            if (id != discount.Id)
            {
                return NotFound();
            }

            try
            {
                await _discountService.UpdateDiscountModel(discount);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DiscountExists(discount.Id))
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
            await _discountService.DeleteById(id);
            return Ok();
        }

        private async Task<bool> DiscountExists(int id)
        {
            return await _discountService.GetById(id) is not null;
        }
    }
}
