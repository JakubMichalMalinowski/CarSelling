using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace CarSelling.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtValidationAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(f => f is DisableJwtValidationAttribute))
            {
                return;
            }

            var token = context.HttpContext.Request.Headers.Authorization;
            var success = AuthenticationHeaderValue
                .TryParse(token, out var parsedToken);
            if (!success)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var handler = new JwtSecurityTokenHandler();
            var result = await handler.ValidateTokenAsync(parsedToken?.Parameter,
                JwtValidation.TokenValidationParameters);

            if (!result.IsValid)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var jwtToken = handler.ReadJwtToken(parsedToken?.Parameter);
            var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");
            if (expClaim is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var claimDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value));
            var nowDate = DateTimeOffset.UtcNow;
            if (claimDate < nowDate)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
