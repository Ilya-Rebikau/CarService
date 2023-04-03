using CarService.UserAPI.Infrastructure;
using CarService.UserAPI.Interfaces;
using CarService.UserAPI.Models.Account;
using CarService.UserAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.UserAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class AccountController : ControllerBase
    {
        private const string AuthorizationKey = "Authorization";
        private readonly IAccountService _service;
        private readonly IJwtService _jwtService;

        public AccountController(IAccountService service, IJwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var registerResult = await _service.RegisterUser(model);
            return registerResult.IdentityResult.Succeeded ? Ok(_jwtService.GetJwt(registerResult.User, registerResult.Roles))
                : BadRequest(registerResult.IdentityResult.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var loginResult = await _service.Login(model);
            return loginResult.SignInResult.Succeeded ? Ok(_jwtService.GetJwt(loginResult.User, loginResult.Roles))
                : Forbid();
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _service.Logout();
            return Ok();
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            var model = await _service.GetEditAccountViewModelForEdit(id);
            return model is null ? NotFound() : Ok(model);
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EditAccountModel model)
        {
            return Ok(await _service.UpdateUserInEdit(model));
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost("validatetoken")]
        public IActionResult ValidateToken([FromHeader(Name = AuthorizationKey)] string token)
        {
            return Ok(_jwtService.ValidateJwt(token));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("getuserid")]
        public async Task<IActionResult> GetUserId([FromHeader(Name = AuthorizationKey)] string token)
        {
            return _jwtService.ValidateJwt(token) ? Ok(await _service.GetUserId(token)) : NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getuser")]
        public async Task<IActionResult> GetUser([FromHeader(Name = AuthorizationKey)] string token)
        {
            return Ok(await _service.GetUser(token));
        }
    }
}