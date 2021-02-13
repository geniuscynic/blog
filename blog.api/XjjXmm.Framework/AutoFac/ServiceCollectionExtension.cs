using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using XjjXmm.Framework.Configuration;

namespace XjjXmm.Framework.AutoFac
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterModule(this ContainerBuilder containerBuilder, string dllName)
        {
          
            var servicesDllFile = Path.Combine(AppContext.BaseDirectory, dllName);
            var assemblysServices = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法

             containerBuilder.RegisterAssemblyTypes(assemblysServices)
                 .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
                 .AsImplementedInterfaces()
                 .InstancePerRequest()
                 .EnableInterfaceInterceptors()
                 .PropertiesAutowired();

        }


      
    }
}
