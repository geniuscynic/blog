using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.LogExtension;
using XjjXmm.FrameWork.Startup;

namespace XjjXmm.Authorize.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var build = CreateHostBuilder(args).Build()     ;
           App.ServiceProvider = build.Services;

              build .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders().AddConsole();

                    //builder.SetMinimumLevel(LogLevel.Debug);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.Inject()
                        .UseStartup<Startup>()
                        
                    .UseSerilogDefault();


                });
    }
}
