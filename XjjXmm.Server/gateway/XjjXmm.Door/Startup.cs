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
using XjjXmm.Core.FrameWork.Cache;
using XjjXmm.Core.SetUp;
using XjjXmm.Core.SetUp.Jwt;
using XjjXmm.Core.SetUp.Swagger;
using XjjXmm.Door.Middleware;

namespace XjjXmm.Door
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
            services.AddCommonSetup(Configuration);

            services.AddSwaggerSetup();

            services.AddSingleton<ICache, DoCareCache>();

            //services.AddHttpClient();
            services.AddHttpClient();
            
            services.AddSingleton<IUrlRewriter, TokenRewriter>();//这里填写前缀与需要转发的地址
            //services.AddJwtSetup("sdfyJWT");
            //services.AddControllers();

            services.AddCors(option => option.AddPolicy("cors", 
                policy => policy.WithMethods("GET", "POST", "HEAD", "OPTIONS")
                    .AllowAnyHeader()
                    //.AllowCredentials()
                    .AllowAnyOrigin()
                    
                
                )
            
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("cors");

            app.UseMiddleware<ProxyMiddleware>();

            app.UseSwaggerMiddlewares();

            app.UseRouting();

            //认证
            app.UseAuthentication();

            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
