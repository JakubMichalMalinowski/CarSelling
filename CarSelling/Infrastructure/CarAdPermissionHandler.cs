﻿using CarSelling.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarSelling.Infrastructure
{
    public class CarAdPermissionHandler
        : AuthorizationHandler<ResourceOwnerRequirement, CarAd>
    {
        private readonly UserPrincipal _userPrincipal;

        public CarAdPermissionHandler(UserPrincipal userPrincipal)
        {
            _userPrincipal = userPrincipal;
        }

        protected async override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceOwnerRequirement requirement,
            CarAd resource)
        {
            var user = await _userPrincipal.GetUserAsync();
            if (user.Id == resource.CreatedBy.Id)
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
