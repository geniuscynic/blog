using System.Linq;
using Autofac;
using Blog.IService;
using Blog.Repository;
using Blog.IRepository;
using Blog.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using XjjXmm.Framework.AutoFac;

namespace Blog.Extension
{
    public static class ServicesSetup
    {
        public static void addService(this IServiceCollection services)
        {

            services.AddScoped<Dbcontext>();
            //services.AddScoped(typeof(IBaseService<>), typeof(BaseServices<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApiMethodRepository, ApiMethodRepository>();

            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IButtonService, ButtonService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IApiMethodService, ApiMethodService>();



            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void RegisterGenericModule(this ContainerBuilder containerBuilder)
        {

            //containerBuilder.RegisterModule("Blog.Service.dll");
            //containerBuilder.RegisterModule("Blog.Repository.dll");

            containerBuilder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

          
        }
    }
}
