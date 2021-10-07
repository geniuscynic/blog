using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XjjXmm.Framework.Configuration;
using XjjXmm.Framework.Filter;

namespace XjjXmm.Framework
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddCommonSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new ConfigurationManager(configuration));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddControllers(configure =>
            {
                // 全局异常过滤
                configure.Filters.Add(typeof(GlobalExceptionsFilter));

            }).AddControllersAsServices();

            return services;
        }

       
    }
}
