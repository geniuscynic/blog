using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using XjjXmm.Framework.Swagger;

namespace XjjXmm.Framework.Permission
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
