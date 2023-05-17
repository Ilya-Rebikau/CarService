using CarService.MainAPI.Interfaces.HttpClients;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarService.MainAPI.Infrastructure
{
    public class JwtMiddleware
    {
        private const string AuthorizationKey = "Authorization";
        private readonly RequestDelegate _next;
        private readonly IUserClient _usersClient;

        public JwtMiddleware(RequestDelegate next, IUserClient usersClient)
        {
            _next = next;
            _usersClient = usersClient;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers.TryGetValue(AuthorizationKey, out var token);
            if (!string.IsNullOrWhiteSpace(token) && token.Count != 0 && await _usersClient.ValidateToken(token))
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
