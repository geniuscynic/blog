using Blog.IService;
using Blog.Repository;
using Blog.IRepository;
using Blog.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Extension
{
    public static class ServicesSetup
    {
        public static void addService(this IServiceCollection services)
        {

            services.AddScoped<Dbcontext>();
            //services.AddScoped(typeof(IBaseService<>), typeof(BaseServices<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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
    }
}
