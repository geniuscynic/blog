using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using XjjXmm.Core.SetUp;
using XjjXmm.Core.SetUp.AutoFac;
using XjjXmm.Core.SetUp.Jwt;
using XjjXmm.Core.SetUp.Swagger;

namespace XjjXmm.Service.Permission
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
