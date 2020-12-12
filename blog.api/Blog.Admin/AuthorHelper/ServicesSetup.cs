using Blog.Core.IService;
using Blog.Repository;
using Blog.Repository.IRepository;
using Blog.Repository.Repository;
using Blog.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Common.Extensions.ServiceExtensions
{
    public static class ServicesSetup
    {
        public static void addService(this IServiceCollection services)
        {

            services.AddScoped<Dbcontext>();
            //services.AddScoped(typeof(IBaseService<>), typeof(BaseServices<>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IButtonService, ButtonService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IApiMethodService, ApiMethodService>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
