using System;
using System.IO;
using System.Reflection;
using Autofac;
using DoCare.Extension.AutoFac;
using DoCare.Extension.Cache;
using DoCare.Extension.Configuration;
using DoCare.Extension.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DoCare.Extension
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddCommonSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new ConfigurationManager(configuration));
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers(configure =>
            {
                // 全局异常过滤
                configure.Filters.Add(typeof(GlobalExceptionsFilter));

            }).AddControllersAsServices().AddNewtonsoftJson();


            return services;
        }

        public static IServiceCollection AddController(this IServiceCollection services, params string[] keys)
        {


            var dlls = ConfigurationManager.GetSection<string[]>(keys);
            foreach (var dllName in dlls)
            {
                var servicesDllFile = Path.Combine(AppContext.BaseDirectory, dllName);
                var assembly = Assembly.LoadFile(servicesDllFile);
                services.AddMvc().AddApplicationPart(assembly);

            }

            return services;
        }


        public static IServiceCollection AddController(this IServiceCollection services, Assembly assembly)
        {
            services.AddMvc().AddApplicationPart(assembly);

            return services;
        }

        public static ContainerBuilder RegisterCommon(this ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.RegisterInstance(new ConfigurationManager(configuration)).AsSelf().SingleInstance().PropertiesAutowired();

            containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance().PropertiesAutowired();

            containerBuilder.RegisterInstance(new DoCareMemoryCache()).As<ICache>().PropertiesAutowired();

            return containerBuilder;
            //containerBuilder.RegisterController(assembly);

            //services.AddController(assembly);
        }
    }
}
