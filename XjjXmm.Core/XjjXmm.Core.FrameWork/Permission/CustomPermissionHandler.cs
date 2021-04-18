using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DoCare.Extension.Permission
{
    public class CustomRequirement : IAuthorizationRequirement
    {
    }

    public class CustomPermissionHandler : AuthorizationHandler<CustomRequirement>
    {
        //private IHttpContextAccessor _accessor;
        //private readonly IRoleService _roleService;

        //public CustomPermissionHandler(IAuthenticationSchemeProvider schemes, IHttpContextAccessor accessor, IRoleService roleService)
        //{
        //    _accessor = accessor;
        //    _roleService = roleService;
        //    Schemes = schemes;
        //}

        //public IAuthenticationSchemeProvider Schemes { get; set; }

        //protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,  CustomRequirement requirement)
        //{

        //    if (context.Resource is RouteEndpoint endpoint)
        //    {
        //        //endpoint.RoutePattern.RequiredValues
        //        //    .TryGetValue("controller", out var _controller);
        //        //endpoint.RoutePattern.RequiredValues
        //        //    .TryGetValue("action", out var _action);

        //        //endpoint.RoutePattern.RequiredValues.TryGetValue("page", out var _page);
        //        // endpoint.RoutePattern.RequiredValues.TryGetValue("area", out var _area);

        //        var route = endpoint.RoutePattern.RawText;
        //        var roles = context.User.Claims
        //            .FirstOrDefault(t => t.Type == ClaimTypes.Role)?.Value
        //            .Split(",")
        //            .ToList();

        //        var test = endpoint.Metadata.FirstOrDefault(t => t is IHttpMethodMetadata) as IHttpMethodMetadata;
        //        var httpMethod = test?.HttpMethods.FirstOrDefault();

                
        //        var result =  await  _roleService.HasApiMethodPermission(roles, route, httpMethod);

        //        if (result)
        //        {
        //            context.Succeed(requirement);

        //            return;
        //        }

        //    }

        //    //var httpContext = _accessor.HttpContext;
        //    ////请求Url
        //    //if (httpContext != null)
        //    //{
        //    //    var questUrl = httpContext.Request.Path.Value.ToLower();
        //    //    //判断请求是否停止
        //    //    var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
        //    //    foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
        //    //    {
        //    //        if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
        //    //        {
        //    //            context.Fail();
        //    //            return;
        //    //        }
        //    //    }
        //    //    //判断请求是否拥有凭据，即有没有登录
        //    //    var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
        //    //    if (defaultAuthenticate != null)
        //    //    {
        //    //        var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
        //    //        //result?.Principal不为空即登录成功
        //    //        if (result?.Principal != null)
        //    //        {

        //    //            httpContext.User = result.Principal;

        //    //            // 获取当前用户的角色信息
        //    //            var currentUserRoles = new List<string>();
        //    //            // ids4和jwt切换
        //    //            // ids4



        //    //        }
        //    //    }

        //    //}

        //    context.Fail();

            
        //}
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            context.Succeed(requirement);
          
        }
    }
}
