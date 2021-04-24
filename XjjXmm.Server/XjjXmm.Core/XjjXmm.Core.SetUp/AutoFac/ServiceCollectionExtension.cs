using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using XjjXmm.Core.FrameWork.Interceptor;
using XjjXmm.Core.SetUp.Configuration;

namespace XjjXmm.Core.SetUp.AutoFac
{
    public static class ServiceCollectionExtension
    {

        public static ContainerBuilder RegisterAssmblyAsSelf(this ContainerBuilder containerBuilder, Assembly assembly)
        {
            containerBuilder.RegisterAssemblyTypes(assembly)
                .AsSelf()
                .InstancePerLifetimeScope()
                //.EnableInterfaceInterceptors()
                // .UsingConstructor(new[] { typeof(IInterceptor[]) })
                .EnableClassInterceptors()
               // .InterceptedBy(typeof(CacheTestInterceptorBase))
                .PropertiesAutowired();

            return containerBuilder;
        }

        public static ContainerBuilder RegisterAssmblyAsImplementedInterfaces(this ContainerBuilder containerBuilder, Assembly assembly)
        {
            containerBuilder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LogInterceptor))
                .PropertiesAutowired();

            return containerBuilder;
        }

        public static ContainerBuilder RegisterController(this ContainerBuilder containerBuilder, Assembly assembly)
        {
            #region 在控制器中使用属性依赖注入，其中注入属性必须标注为public
            //在控制器中使用属性依赖注入，其中注入属性必须标注为public
            
            //var assemblysServices = Assembly.Load(servicesDllFile);//直接采用加载文件的方法

            var controllersTypesInAssembly = assembly.GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            containerBuilder.RegisterTypes(controllersTypesInAssembly)
                .PropertiesAutowired();
                //.EnableInterfaceInterceptors()
                //.EnableClassInterceptors()
                //.InterceptedBy(typeof(LogInterceptor));
            #endregion

            return containerBuilder;
        }

        public static ContainerBuilder RegisterCommon(this ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.RegisterType<LogInterceptor>();

            //containerBuilder.RegisterType<LogInterceptor>();
            //containerBuilder.RegisterInstance(new ConfigurationManager(configuration)).AsSelf().SingleInstance().PropertiesAutowired();

            //containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance().PropertiesAutowired();

            //containerBuilder.RegisterInstance(new DoCareMemoryCache()).As<ICache>().PropertiesAutowired();

            return containerBuilder;
            //containerBuilder.RegisterController(assembly);

            //services.AddController(assembly);
        }



    }
}
