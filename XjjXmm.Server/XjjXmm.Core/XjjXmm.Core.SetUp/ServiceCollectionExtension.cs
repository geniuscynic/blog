
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XjjXmm.Core.FrameWork.Filter;
using XjjXmm.Core.SetUp.Configuration;

namespace XjjXmm.Core.SetUp
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
                configure.Filters.Add(typeof(MvcActionFilter));

            }).AddControllersAsServices();//.AddNewtonsoftJson();


            return services;
        }

        
    }
}
