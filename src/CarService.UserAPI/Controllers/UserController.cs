using CarService.UserAPI.Infrastructure;
using CarService.UserAPI.Interfaces;
using CarService.UserAPI.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.UserAPI.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers([FromBody] int pageNumber)
        {
            return Ok(await _service.GetUsers(pageNumber));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserModel model)
        {
            return Ok(await _service.CreateUser(model));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            var model = await _service.GetEditUserViewModel(id);
            return model is null ? NotFound() : Ok(model);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EditUserModel model)
        {
            return Ok(await _service.UpdateUserInEdit(model));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var deletedId = await _service.DeleteUser(id);
            return string.IsNullOrEmpty(deletedId) ? NotFound() : Ok(deletedId);
        }

        [HttpGet("changepassword/{id}")]
        public async Task<IActionResult> ChangePassword([FromRoute] string id)
        {
            var model = await _service.GetChangePasswordViewModel(id);
            return model is null ? NotFound() : Ok(model);
        }

        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            return Ok(await _service.ChangePassword(model, HttpContext));
        }

        [HttpGet("editroles/{id}")]
        public async Task<IActionResult> EditRoles([FromRoute] string id)
        {
            var model = await _service.GetChangeRoleViewModel(id);
            return model is null ? NotFound() : Ok(model);
        }

        [HttpPut("editroles")]
        public async Task<IActionResult> EditRoles([FromBody] ChangeRoleModel model)
        {
            await _service.EditRoles(model);
            return Ok();
        }
    }
}