using CarService.UserAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarService.UserAPI.Infrastructure
{
    internal class JwtMiddleware
    {
        private const string AuthorizationKey = "Authorization";
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;
        public JwtMiddleware(RequestDelegate next, IJwtService jwtTokenService)
        {
            _next = next;
            _jwtService = jwtTokenService;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers.TryGetValue(AuthorizationKey, out var token);
            if (_jwtService.ValidateJwt(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var claims = jwtSecurityToken.Claims;
                var identity = new ClaimsIdentity(claims);
                var principalClaims = new ClaimsPrincipal(identity);
                context.User = principalClaims;
            }

            await _next(context);
        }
    }
}
