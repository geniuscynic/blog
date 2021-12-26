using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SqlSugar;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.Swagger;
using XjjXmm.FrameWork.ToolKit.Captcha;
using Admin.Repository;
using Admin.Api.tool;

namespace Admin.Api
{
    public class Startup
    {
        public Startup()
        {
           // Configuration = AppDomainSetup;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new LongJsonConverter());
               // options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               // options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
            });
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Admin.Api", Version = "v1" });
            //});

            var c = App.Configuration.GetConfig("db:connectionString");
            var sqlSugar = new SqlSugarScope(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.MySql,
                    ConnectionString = App.Configuration.GetConfig("db:connectionString"),
                    IsAutoCloseConnection = true,
                },
                db =>
                {
                    //单例参数配置，所有上下文生效
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        App.Logger.Debug($"Sql:{sql}\r\n paramters: {JsonConvert.SerializeObject(pars)}");
                        //Console.WriteLine(sql);//输出sql
                        //Console.WriteLine(string.Join(",", pars?.Select(it => it.ParameterName + ":" + it.Value)));//参数
                    };
                });
            services.AddSingleton<ISqlSugarClient>(sqlSugar);


            services.AddScoped<ICaptcha, DefaultCaptcha>();
            services.AddScoped<ICache, XjjxmmMemoryCache>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               // app.UseSwagger();
               // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Admin.Api v1"));
            }

            app.UseSwaggerMiddlewares();

            app.UseRouting();

            //认证
            app.UseAuthentication();

            //授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
