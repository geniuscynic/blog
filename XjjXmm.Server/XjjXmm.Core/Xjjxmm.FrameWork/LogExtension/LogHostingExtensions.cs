using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace XjjXmm.FrameWork.LogExtension
{
    /// <summary>
    /// Serilog 日志拓展
    /// </summary>
    public static class LogHostingExtensions
    {
        /// <summary>
        /// 添加默认日志拓展
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="configAction"></param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder UseDefaultLog(this IWebHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureLogging((hostingContext, builder) =>
            {

                //该方法需要引入Microsoft.Extensions.Logging名称空间
                builder.AddFilter("System", LogLevel.Error); //过滤掉系统默认的一些日志
                builder.AddFilter("Microsoft", LogLevel.Error);//过滤掉系统默认的一些日志

                //添加Log4Net
                //var path = Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config");
                //不带参数：表示log4net.config的配置文件就在应用程序根目录下，也可以指定配置文件的路径
                //需要添加nuget包：Microsoft.Extensions.Logging.Log4Net.AspNetCore
                //builder.AddLog4Net(path);
                builder.AddLog4Net();
            });

            return hostBuilder;
        }

        /// <summary>
        /// 添加默认日志拓展
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="configAction"></param>
        /// <returns>IWebHostBuilder</returns>
        //public static IWebHostBuilder UseSerilogDefault(this IWebHostBuilder hostBuilder, Action<LoggerConfiguration> configAction = default)
        //{
        //    hostBuilder.UseSerilog((context, configuration) =>
        //    {
        //        // 加载配置文件
        //        var config = configuration
        //            .ReadFrom.Configuration(context.Configuration)
        //            .Enrich.FromLogContext();

        //        if (configAction != null) configAction.Invoke(config);
        //        else
        //        {
        //            // 判断是否有输出配置
        //            var hasWriteTo = context.Configuration["Serilog:WriteTo:0:Name"];
        //            if (hasWriteTo == null)
        //            {
        //                config.MinimumLevel.Debug()
        //                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
        //                  .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "application.log"),
        //                      LogEventLevel.Debug,
        //                      rollingInterval: RollingInterval.Day,
        //                      retainedFileCountLimit: null,
        //                      encoding: Encoding.UTF8);
        //            }
        //        }
        //    });

        //    return hostBuilder;
        //}

        /// <summary>
        /// 添加默认日志拓展
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        //public static IHostBuilder UseSerilogDefault(this IHostBuilder builder, Action<LoggerConfiguration> configAction = default)
        //{
        //    builder.UseSerilog((context, configuration) =>
        //    {
        //        // 加载配置文件
        //        var config = configuration
        //            .ReadFrom.Configuration(context.Configuration)
        //            .Enrich.FromLogContext();

        //        if (configAction != null) configAction.Invoke(config);
        //        else
        //        {
        //            // 判断是否有输出配置
        //            var hasWriteTo = context.Configuration["Serilog:WriteTo:0:Name"];
        //            if (hasWriteTo == null)
        //            {
        //                config.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
        //                  .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "application.log"), LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, encoding: Encoding.UTF8);
        //            }
        //        }
        //    });

        //    return builder;
        //}
    }
}