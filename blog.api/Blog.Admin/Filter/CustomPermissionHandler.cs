using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Blog.API.Filter
{
    public class CustomPermissionHandler : AuthorizationHandler<CustomRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            
            if (context.Resource is RouteEndpoint endpoint)
            {
                endpoint.RoutePattern.RequiredValues
                    .TryGetValue("controller", out var _controller);
                endpoint.RoutePattern.RequiredValues
                    .TryGetValue("action", out var _action);

                //endpoint.RoutePattern.RequiredValues.TryGetValue("page", out var _page);
               // endpoint.RoutePattern.RequiredValues.TryGetValue("area", out var _area);

                var isAuthenticated = context.User.Identity.IsAuthenticated;

             
                    context.Succeed(requirement);
                
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
