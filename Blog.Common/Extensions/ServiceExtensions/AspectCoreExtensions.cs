using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Blog.Common.Extensions.AOP;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Extensions.ServiceExtensions
{
    public static class AspectCoreExtensions
    {
        public static void AddAspectCore(this IServiceCollection services)
        {
            services.ConfigureDynamicProxy(config =>
            {
                //TestInterceptor拦截器类
                //拦截代理所有Service结尾的类
                config.Interceptors.AddTyped<LogInterceptorAttribute>();
            });
            //services.BuildAspectInjectorProvider();
        }
    }
}
