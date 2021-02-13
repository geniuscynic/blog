using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Blog.API.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XjjXmm.Framework.AutoFac;
using XjjXmm.Framework.Logger;

namespace Blog.API
{
    public class Program
    {

        public static void Main(string[] args)
        {

            //var tsTypes = typeof(BlogController).Assembly.GetTypes()
            //    .Where(t => t.Namespace == "Blog.API.Controllers" && t.FullName.EndsWith("Controller"))
            //     .SelectMany(t => t.GetMethods())
            //    .Where(t => t.DeclaringType.Namespace == "Blog.API.Controllers")
            //    .Select(t=> new
            //    {
            //        name = t.DeclaringType.Name,
            //        action = t.Name,
            //        parments = t.GetParameters(),
            //        other = t
            //    })
            //    .ToList();

                
            

            //获取所有方法 
            //System.Reflection.MethodInfo[] methods = t.GetMethods();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .AddLogNetBuilder()
                .AddAutoFaceBuilder();
    }
}
