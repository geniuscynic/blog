using Autofac;
using AutoMapper;
using Blog.API.AuthorHelper;
using Blog.Common;
using Blog.Common.Extensions.Middlewares;
using Blog.Extension;
using Blog.Extension.Extensions.ServiceExtensions;
using Blog.IService;
using Blog.Repository;
using Blog.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using XjjXmm.Framework;
using XjjXmm.Framework.AutoFac;
using XjjXmm.Framework.AutoMap;
using XjjXmm.Framework.Configuration;
using XjjXmm.Framework.Filter;
using XjjXmm.Framework.Jwt;
using XjjXmm.Framework.Swagger;


namespace Blog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {

            //containerBuilder.RegisterModule("Blog.Service.dll");
            //containerBuilder.RegisterModule("Blog.Repository.dll");

            containerBuilder.RegisterGenericModule();

            containerBuilder
                .RegisterModulesByKey("blog", "ModuleDll");
            //.RegisterControllerByKey("blog", "ControllerDll");

            //var servicesDllFile = Path.Combine(AppContext.BaseDirectory, "Blog.Repository.dll");
            //var assemblysServices = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法

            //var servicesDllFile2 = Path.Combine(AppContext.BaseDirectory, "Blog.IRepository.dll");
            //var assemblysServices2 = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法

            //containerBuilder.RegisterAssemblyTypes(assemblysServices2, assemblysServices)
            //    //.Where(t => t.Name.EndsWith("Repository"))
            //    .AsSelf()
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope()
            //    .EnableInterfaceInterceptors()
            //    .PropertiesAutowired(); ;

            #region 在控制器中使用属性依赖注入，其中注入属性必须标注为public
            //在控制器中使用属性依赖注入，其中注入属性必须标注为public
            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            containerBuilder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();
            #endregion
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //string issue = Configuration["JWT:Issue"]; // "Issuer";

            //var JwtTokenSetting = new JwtTokenSetting();
            //Configuration.GetSection("JWT").Bind(JwtTokenSetting);

            // var setting = services.Configure<JwtTokenSetting>(Configuration.GetSection("JWT"));

            //var loggingOptions = Configuration.GetSection("JWT").Get<JwtTokenSetting>();

            //loggingOptions.GetClickSkew();
            //services.ConfigureDynamicProxy(config =>
            //{
            //    //TestInterceptor拦截器类
            //    //拦截代理所有Service结尾的类
            //    config.Interceptors.AddTyped<LogInterceptor>();
            //});
            //services.BuildAspectInjectorProvider();
            //services.AddAspectCore();
            //services.AddAutoMapper(typeof(Startup));//这是AutoMapper的2.0新特性
            //services.AddAutoMapperSetup();
            //services.AddSingleton(new AppSettingHelper(Configuration));
            //services.AddAutoMapper(typeof(CustomProfile));
            

            services
                .AddCommonSetup(Configuration)
                .AddSwaggerSetup()
                .AddAutoMapperByKey("blog", "AutoMapConfig")
                .AddPermissionSetup();

            services.AddSqlsugarSetup();

            services.AddScoped<Dbcontext>();
            //services.addService();
            //var ServicesDllFile = Path.Combine(basePath, "Exercise.Services.dll");//获取注入项目绝对路径
            //var assemblysServices = Assembly.LoadFile(ServicesDllFile);//直接采用加载文件的方法
            //containerBuilder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();





            //services.AddSwaggerSetup();

            services.AddJwtSetup("JWT");

            //services.ConfigureDynamicProxy();

            //services.AddMiniProfiler(options =>
            //{
            //    options.RouteBasePath = "/profiler";//注意这个路径要和下边 index.html 脚本配置中的一致，
            //    //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);

            //});


            services.AddCors();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Dbcontext dbcontext)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerMiddlewares();
            //app.UseSwaggerMiddlewares();

            app.UseRouting();

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()//Ensures that the policy allows any header.
                        .AllowAnyMethod()
                        

            );

            app.UseStaticFiles();

            //app.UseJwtTokenAuth();
            //认证
            app.UseAuthentication();

            //授权
            app.UseAuthorization();


            app.UseSeedDataMildd(dbcontext);

            //app.UseMiniProfiler();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
