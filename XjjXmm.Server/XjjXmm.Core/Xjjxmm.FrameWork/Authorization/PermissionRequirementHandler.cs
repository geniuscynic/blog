using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace XjjXmm.FrameWork.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class XjjXmmPermissionAuthorizationHandler : AuthorizationHandler<XjjXmmPermissionAuthorizationRequirement>
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="context"></param>
        ///// <param name="requirement"></param>
        ///// <returns></returns>
        //protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        //{
        //    HttpContext httpContext = context.Resource as HttpContext;

        //    context.Succeed(requirement);


        //    return Task.CompletedTask;
        //}
    }
}
