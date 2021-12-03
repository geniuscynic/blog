using Admin.Core.Common.Configs;
using Admin.Core.Common.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.LogExtension;
using XjjXmm.FrameWork.Startup;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

//using NLog.Extensions.Logging;

namespace Admin.Core
{
    public class Program
    {
        private static AppConfig appConfig; //= App.Configuration.GetSection<AppConfig>() ?? new AppConfig();

        public static async Task<int> Main(string[] args)
        {
           var logger = LoggerHelper<Program>.GetLogger();
           // var logger = LogManager.GetCurrentClassLogger();
            try
            {
                //Console.WriteLine(" launching...");
                var host = CreateHostBuilder(args).Build().SetUp();
                
               // Console.WriteLine($"\r\n {string.Join("\r\n ", appConfig.Urls)}\r\n");
                await host.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                logger.Error("Stopped program because of exception",ex);
                return 1;
            }
            finally
            {
                //LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //使用logconfig.json配置，默认使用nlog.config
            //var logConfig = new ConfigHelper().Load("logconfig", reloadOnChange: true).GetSection("nLog");
            //LogManager.Configuration = new NLogLoggingConfiguration(logConfig);

            return Host.CreateDefaultBuilder(args)
                //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                //.SetUp()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //Environments.Development
                    //webBuilder.UseEnvironment(Ev)
                    webBuilder.Inject();

                    appConfig = App.Configuration.GetSection<AppConfig>("app") ?? new AppConfig();
                    //.UseEnvironment(Environments.Production)
                    webBuilder.UseStartup<Startup>()
                        .ConfigureAppConfiguration((host, config) =>
                        {
                            if (appConfig.RateLimit)
                            {
                                config.AddJsonFile("./configs/ratelimitconfig.json", optional: false,
                                        reloadOnChange: true)
                                    .AddJsonFile(
                                        $"./configs/ratelimitconfig.{host.HostingEnvironment.EnvironmentName}.json",
                                        optional: true, reloadOnChange: true);
                            }
                        })
                        .UseUrls(appConfig.Urls);
                })
                .SetUp();
            //.ConfigureLogging(logging =>
            //{
            //    logging.ClearProviders();
            //    logging.SetMinimumLevel(LogLevel.Trace);
            //})
            //.UseNLog();
        }
    }
}