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
using DoCare.Zkzx.Core.Database;
using DoCare.Zkzx.Core.Database.Utility;
using Newtonsoft.Json;
using Serilog;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.Swagger;

namespace XjjXmm.Authorize.Api
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

            services.AddControllers();

            // services.AddSwaggerSetup();

            var connectionString = App.GetConfig("ConnectionStrings:connectionString");
            var providerName = App.GetConfig("ConnectionStrings:providerName");

            services.AddTransient(_ => new Dbclient(connectionString, providerName, new Aop()
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

                })
            );
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

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
