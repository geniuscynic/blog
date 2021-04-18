using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using XjjXmm.Framework.Configuration;

namespace XjjXmm.Framework.AutoMap
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, string dllName)
        {
            var servicesDllFile = Path.Combine(AppContext.BaseDirectory, dllName);
            var assemblysServices = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法

            services.AddAutoMapper(assemblysServices);

            return services;

        }

        public static IServiceCollection AddAutoMapperByKey(this IServiceCollection services, params string[] keys)
        {
            var dlls = ConfigurationManager.GetSection<string[]>(keys);
            foreach (var dllName in dlls)
            {
                services.AddAutoMapper(dllName);

            }

            return services;

        }

    }
}
