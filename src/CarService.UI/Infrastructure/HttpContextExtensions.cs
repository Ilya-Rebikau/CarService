namespace CarService.UI.Infrastructure
{
    public static class HttpContextExtensions
    {
        private const string CookiesKey = "JwtTokenCookie";

        public static string GetJwtToken(this HttpContext httpContext)
        {
            httpContext.Request.Cookies.TryGetValue(CookiesKey, out var token);
            return token;
        }

        public static void AppendCookies(this HttpContext httpContext, string token)
        {
            httpContext.Response.Cookies.Append(CookiesKey, token, new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.Lax,
            });
        }

        public static void DeleteCookies(this HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(CookiesKey);
        }
    }
}
