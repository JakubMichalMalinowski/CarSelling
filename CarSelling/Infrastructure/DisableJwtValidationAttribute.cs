using Microsoft.AspNetCore.Mvc.Filters;

namespace CarSelling.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DisableJwtValidationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            return;
        }
    }
}
