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
           CreateHostBuilder(args)
               .Build()
               .SetUp()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders().AddSerilog();

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
