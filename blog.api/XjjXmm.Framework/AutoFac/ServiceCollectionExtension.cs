using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using XjjXmm.Framework.Configuration;

namespace XjjXmm.Framework.AutoFac
{
    public static class ServiceCollectionExtension
    {
        public static ContainerBuilder RegisterModule(this ContainerBuilder containerBuilder, string dllName)
        {
          
            var servicesDllFile = Path.Combine(AppContext.BaseDirectory, dllName);
            var assemblysServices = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法

            
            containerBuilder.RegisterAssemblyTypes(assemblysServices)
                 //.Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
                 //.AsSelf()
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope()
                 //.InstancePerRequest()
                 //.InstancePerRequest()
                 .EnableInterfaceInterceptors()
                 .PropertiesAutowired();

            containerBuilder.RegisterAssemblyTypes(assemblysServices)
                .Where(t => t.Name.EndsWith("context"))
                .AsSelf()
                //.InstancePerLifetimeScope()
                //.InstancePerRequest()
                //.InstancePerRequest()
                .EnableClassInterceptors()
                .PropertiesAutowired();

            return containerBuilder;

        }

        public static ContainerBuilder RegisterModulesByKey(this ContainerBuilder containerBuilder, params string[] keys)
        {

            var dlls = ConfigurationManager.GetSection<string[]>(keys);
            foreach (var dllName in dlls)
            {
                containerBuilder.RegisterModule(dllName);

            }

            return containerBuilder;

        }


        public static ContainerBuilder RegisterController(this ContainerBuilder containerBuilder, string dllName)
        {
            #region 在控制器中使用属性依赖注入，其中注入属性必须标注为public
            //在控制器中使用属性依赖注入，其中注入属性必须标注为public
            var servicesDllFile = Path.Combine(AppContext.BaseDirectory, dllName);
            var assemblysServices = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法

            var controllersTypesInAssembly = assemblysServices.GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            containerBuilder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();
            #endregion

            return containerBuilder;
        }

        public static ContainerBuilder RegisterControllerByKey(this ContainerBuilder containerBuilder, params string[] keys)
        {
            var dlls = ConfigurationManager.GetSection<string[]>(keys);
            foreach (var dllName in dlls)
            {
                containerBuilder.RegisterController(dllName);

            }

            return containerBuilder;
        }


    }
}
