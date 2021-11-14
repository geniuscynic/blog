using System.Reflection;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using XjjXmm.FrameWork.Aop;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Filter;
using XjjXmm.FrameWork.Startup;
using XjjXmm.FrameWork.Swagger;
using XjjXmm.FrameWork.ToolKit;

[assembly: HostingStartup(typeof(XjjXmm.FrameWork.Startup.HostingStartup))]
namespace XjjXmm.FrameWork.Startup
{
    /// <summary>
    /// 配置程序启动时自动注入
    /// </summary>

    public sealed class HostingStartup : IHostingStartup
    {
        /// <summary>
        /// 配置应用启动
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IWebHostBuilder builder)
        {
            // 应用初始化服务
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddSwaggerSetup();

                // 存储配置对象
                App.Configuration = hostContext.Configuration;

                // 注册 HttpContextAccessor 服务
                services.AddHttpContextAccessor();

                services.AddDependencyInjection();

               // var a = DependencyContext.Default.RuntimeLibraries;
                //var b = ReflectKit.AllAssemblies();
                services.Configure<MvcOptions>(option =>
                {
                    option.Filters.Add(typeof(MvcActionFilter));
                    option.Filters.Add(typeof(GlobalExceptionsFilter));
                });

                services.ConfigureDynamicProxy(config =>
                {
                    config.Interceptors.AddTyped<CustomInterceptor>();
                });
                //App.ServiceProvider = services.BuildServiceProvider();
            });

            

            //builder.Configure((context, applicationBuilder) =>
            //{
            //    applicationBuilder.UseSwaggerMiddlewares();
            //});

        }
    }
}