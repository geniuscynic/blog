using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using XjjXmm.Core.SetUp.AutoFac;

namespace XjjXmm.Service.Permission
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()//最小的输出单位是Debug级别的
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//将Microsoft前缀的日志的最小输出级别改成Information
                .Enrich.FromLogContext()
                //.WriteTo.Console("logFiles/pangjianxin.{Date}.txt",RollingInterval.Day)
               // .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "Logs/logs.txt"))
                .WriteTo.Console()
                .CreateLogger();
            //将日志输出到目标路径，文件的生成方式为每天生成一个文件


            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
               // return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
              //  return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog()
                .AddAutoFaceBuilder();
    }
}
