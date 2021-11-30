using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using XjjXmm.FrameWork.Authorization;
using XjjXmm.FrameWork.DependencyInjection;

namespace XjjXmm.Authorize.Api.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    [Injection]
    public class PermissionAuthorizationHandler : XjjXmmPermissionAuthorizationHandler
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="context"></param>
        ///// <param name="requirement"></param>
        ///// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, XjjXmmPermissionAuthorizationRequirement requirement)
        {
            HttpContext httpContext = context.Resource as HttpContext;

            context.Succeed(requirement);


            return Task.CompletedTask;
        }
    }
}
