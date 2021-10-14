using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using XjjXmm.FrameWork.ToolKit;

namespace XjjXmm.FrameWork.DependencyInjection
{
   internal static class DependencyInjectionExtension
    {
        internal static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            foreach (var type in GetExportedType())
            {
                var attr = type.GetCustomAttribute<InjectionAttribute>();
                if (attr == null)
                {
                    continue;
                }

                switch (attr.Type)
                {
                    case InjectionType.Transient:
                        services.AddTransient(type);
                        break;

                    case InjectionType.Scoped:
                        services.AddScoped(type);
                        break;

                    case InjectionType.Singleton:
                        services.AddSingleton(type);
                        break;
                   
                }
                //var b = type.GetMethods();
                
                var types = type.GetInterfaces();

                foreach (var type1 in types)
                {
                    switch (attr.Type)
                    {
                        case InjectionType.Transient:
                            services.AddTransient(type1, type);
                            break;

                        case InjectionType.Scoped:
                            services.AddScoped(type1, type);
                            break;

                        case InjectionType.Singleton:
                            services.AddSingleton(type1, type);
                            break;

                    }
                }
                
            }
            
            //..Where(t => t.ExportedTypes.GetCustomAttribute<InjectionAttribute>() != null);


            return services;
        }

        private static IEnumerable<Type> GetExportedType()
        {
            var assmblies = ReflectKit.AllAssemblies();

            foreach (var assmbly in assmblies)
            {
                //var b = assmbly.ExportedTypes.Where(t => t.GetCustomAttribute<InjectionAttribute>() != null);
                foreach (var assmblyExportedType in assmbly.ExportedTypes)
                {
                    yield return assmblyExportedType;
                }
            }
        }
    }
}
