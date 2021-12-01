using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using XjjXmm.FrameWork.LogExtension;
using XjjXmm.FrameWork.ToolKit;

namespace XjjXmm.FrameWork.Startup
{
    /// <summary>
    /// 主机构建器拓展类
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Web 主机注入
        /// </summary>
        /// <param name="hostBuilder">Web主机构建器</param>
        /// <param name="assemblyName">外部程序集名称</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder Inject(this IWebHostBuilder hostBuilder,string environments = "", string assemblyName = default)
        {
            var frameworkPackageName = assemblyName ?? ReflectKit.GetAssemblyName(typeof(HostBuilderExtensions));
            hostBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, frameworkPackageName);
            hostBuilder.UseDefaultLog();

            
            App.Configuration.Scan(environments);
            return hostBuilder;
        }

        /// <summary>
        /// Web 主机注入
        /// </summary>
        /// <param name="host">Web主机构建器</param>
        /// <returns>IWebHostBuilder</returns>
        public static IHost SetUp(this IHost host)
        {
            App.ServiceProvider = host.Services;
            
            return host;
        }

        /// <summary>
        /// Web 主机注入
        /// </summary>
        /// <param name="host">Web主机构建器</param>
        /// <returns>IWebHostBuilder</returns>
        public static IHostBuilder SetUp(this IHostBuilder host)
        {
            host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());

            return host;
        }


    }
}