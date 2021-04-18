using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace DoCare.Extension.Permission
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddPermissionSetup(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, CustomPermissionHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("xjjXmmPermission", policy =>
                    policy.Requirements.Add(new CustomRequirement()));
            });

            return services;
        }
    }
}
