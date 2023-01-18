namespace CarSelling.Infrastructure
{
    public class JwtSettingsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtSettingsMiddleware(RequestDelegate next,
            IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            JwtValidation.SetTokenValidationParameters(_configuration);
            await _next(context);
        }
    }
}
