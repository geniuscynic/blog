using Microsoft.Extensions.DependencyInjection;

namespace DoCare.Extension.MiniProfiler
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddJwtSetup(this IServiceCollection services)
        {
            //services.AddMiniProfiler(options =>
            //{
            //    options.RouteBasePath = "/profiler";//注意这个路径要和下边 index.html 脚本配置中的一致，
            //    //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);

            //});

            return services;
        }
    }
}
