using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Extensions.DependencyInjection;
using AutoMapper.Internal;
using Blog.API.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

                
            

            //��ȡ���з��� 
            //System.Reflection.MethodInfo[] methods = t.GetMethods();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging((hostingContext, builder) =>
            {
                //���˵�ϵͳĬ�ϵ�һЩ��־
                //builder.AddFilter("System", LogLevel.Error);
                //builder.AddFilter("Microsoft", LogLevel.Error);
                //builder.AddFilter("Blog.Core.AuthHelper.ApiResponseHandler", LogLevel.Error);

                //�������ļ�
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config");


                builder.AddLog4Net(path);
            }).
            UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());
    }
}
