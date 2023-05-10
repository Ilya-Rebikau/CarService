using CarService.UserAPI.Infrastructure;
using CarService.UserAPI.Interfaces;
using CarService.UserAPI.Models.Account;
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var registerResult = await _service.RegisterUser(model);
            return registerResult.IdentityResult.Succeeded ? Ok(_jwtService.GetJwt(registerResult.User, registerResult.Roles))
                : throw new MyException("Ошибка регистрации. user уже существует.");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var loginResult = await _service.Login(model);
            return loginResult.SignInResult.Succeeded? Ok(_jwtService.GetJwt(loginResult.User, loginResult.Roles))
                : throw new MyException("Неверный логин и/или пароль");
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

        [AllowAnonymous]
        [HttpPost("validatetoken")]
        public IActionResult ValidateToken([FromHeader(Name = AuthorizationKey)] string token)
        {
            return Ok(_jwtService.ValidateJwt(token));
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpGet("getaccountmodel")]
        public async Task<IActionResult> GetAccountModel([FromHeader(Name = AuthorizationKey)] string token)
        {
            return Ok(await _service.GetAccountModel(token));
        }

        [Authorize(Roles = "admin, manager, user")]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordInPersonalAccountModel model)
        {
            return Ok(await _service.ChangePassword(model));
        }

        [AllowAnonymous]
        [HttpGet("getuseremail")]
        public async Task<IActionResult> GetUserEmail([FromQuery] string userId)
        {
            return Ok(await _service.GetUserEmail(userId));
        }

        [AllowAnonymous]
        [HttpGet("getusername")]
        public async Task<IActionResult> GetUserName([FromQuery] string userId)
        {
            return Ok(await _service.GetUserName(userId));
        }

        [AllowAnonymous]
        [HttpGet("getuserphoto")]
        public async Task<IActionResult> GetUserPhoto([FromQuery] string userId)
        {
            return Ok(await _service.GetUserPhoto(userId));
        }
    }
}