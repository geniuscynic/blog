using System;
using System.Reflection;
using Autofac;
using DoCare.Zkzx.Core.Database;
using DoCare.Zkzx.Core.Database.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using XjjXmm.Core.FrameWork.Interceptor;
using XjjXmm.Core.SetUp;
using XjjXmm.Core.SetUp.AutoFac;
using XjjXmm.Core.SetUp.Configuration;
using XjjXmm.Core.SetUp.Jwt;
using XjjXmm.Core.SetUp.Swagger;

namespace Permission.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
          
        }

        public IConfiguration Configuration { get; }

       

        // private IServiceCollection Services { get; set; }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {

            
            
            containerBuilder.RegisterCommon(Configuration);

            
            
            containerBuilder.RegisterController(typeof(Startup).Assembly);

            

            var connectionString = ConfigurationManager.Appsetting($"ConnectionStrings:User:connectionString");
            var providerName = ConfigurationManager.Appsetting($"ConnectionStrings:User:providerName");

            containerBuilder.Register<ILogger>(t => Log.Logger).PropertiesAutowired();

            containerBuilder.Register(c => new Dbclient(connectionString, providerName, new Aop()
                    {
                        OnError = (sql, paramter, ex) =>
                        {
                            //Log.Information("Sql: \r\n{0}", sql);
                            Log.Logger.Debug($"Sql:  {sql}, \r\n paramter: {JsonConvert.SerializeObject(paramter)}");
                            Log.Logger.Error(ex, "sqlerror");
                            //Console.WriteLine(sql);
                        },
                        OnExecuting = (sql, paramter) =>
                        {
                            //Console.WriteLine(sql);
                            Log.Logger.Debug($"Sql:  {sql}, \r\n paramter: {JsonConvert.SerializeObject(paramter)}");
                        },

                    }
                ))
                .AsSelf()
                .InstancePerLifetimeScope();


            containerBuilder
                .RegisterAssmblyAsImplementedInterfaces(Assembly.Load("Permission.Repository"))
                .RegisterAssmblyAsImplementedInterfaces(Assembly.Load("Permission.Service"));
            // Services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //this.Services = services;

            services
                .AddCommonSetup(Configuration)
                .AddSwaggerSetup();

            services.AddJwtSetup("JWT");

            services.AddCors();


            //.AddAutoMapperByKey("blog", "AutoMapConfig")
            //.AddPermissionSetup();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerMiddlewares();

            app.UseRouting();

            //ÈÏÖ¤
            app.UseAuthentication();

            //ÊÚÈ¨
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
