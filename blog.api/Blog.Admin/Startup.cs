using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AutoMapper;
using Blog.API.AuthorHelper;
using Blog.API.Filter;
using Blog.Common.Extensions.AutoMapper;
using Blog.Common.Extensions.Middlewares;
using Blog.Common.Extensions.ServiceExtensions;
using Blog.Core.IService;
using Blog.Repository;
using Blog.Repository.IRepository;
using Blog.Repository.Repository;
using Blog.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar;
using StackExchange.Profiling.Storage;
using Swashbuckle.AspNetCore.Filters;

namespace Blog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.ConfigureDynamicProxy(config =>
            //{
            //    //TestInterceptor��������
            //    //���ش�������Service��β����
            //    config.Interceptors.AddTyped<LogInterceptor>();
            //});
            //services.BuildAspectInjectorProvider();
            services.AddAspectCore();
            //services.AddAutoMapper(typeof(Startup));//����AutoMapper��2.0������
            services.AddAutoMapperSetup();
            services.AddSingleton(new AppSettingHelper(Configuration));
            services.AddSqlsugarSetup();


            services.addService();


            services.AddControllers(configure =>
            {
                // ȫ���쳣����
                configure.Filters.Add(typeof(GlobalExceptionsFilter));
                configure.Filters.Add(typeof(MyActionFilterAttribute));
            });



            services.AddSwaggerSetup();

            services.AddJwtSetup();

            //services.ConfigureDynamicProxy();

            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";//ע�����·��Ҫ���±� index.html �ű������е�һ�£�
                //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);

            });


            services.AddCors();

            services.AddSingleton<IAuthorizationHandler, CustomPermissionHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("mypermission", policy =>
                    policy.Requirements.Add(new CustomRequirement()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Dbcontext dbcontext)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"xjjxmm v1");

                c.RoutePrefix = "";


                var streamHtml = ReflectionExtensions.GetTypeInfo(GetType()).Assembly.GetManifestResourceStream("Blog.API.index.html");

                c.IndexStream = () => streamHtml;
            });


            


            app.UseRouting();

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()//Ensures that the policy allows any header.
                        .AllowAnyMethod()
                        

            );

            app.UseStaticFiles();

            //app.UseJwtTokenAuth();
            //��֤
            app.UseAuthentication();

            //��Ȩ
            app.UseAuthorization();


            app.UseSeedDataMildd(dbcontext);

            app.UseMiniProfiler();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
