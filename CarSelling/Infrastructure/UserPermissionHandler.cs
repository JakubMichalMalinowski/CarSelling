using CarSelling.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarSelling.Infrastructure
{
    public class UserPermissionHandler
        : AuthorizationHandler<ResourceOwnerRequirement, User>
    {
        private readonly UserPrincipal _userPrincipal;

        public UserPermissionHandler(UserPrincipal userPrincipal)
        {
            _userPrincipal = userPrincipal;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceOwnerRequirement requirement,
            User resource)
        {
            var user = await _userPrincipal.GetUserAsync();
            if (user.Id == resource.Id)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
