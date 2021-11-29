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
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Service;
using XjjXmm.DataBase;
using XjjXmm.DataBase.Utility;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.Jwt;
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

            var connectionString = App.GetConfig("ConnectionString:connectionString");
            var providerName = App.GetConfig("ConnectionString:dbType");

            services.AddScoped(_ => new DbClient(connectionString, providerName, new Aop()
            {
                OnError = (sql, paramter, ex) =>
                {
                        //Log.Information("Sql: \r\n{0}", sql);
                      //  Log.Debug($"Sql:  {sql}, \r\n paramter: {JsonConvert.SerializeObject(paramter)}");
                   // Log.Error(ex, "sqlerror");
                        //Console.WriteLine(sql);
                    },
                OnExecuting = (sql, paramter) =>
                {
                        //Console.WriteLine(sql);
                       // Log.Debug($"Sql:  {sql}, \r\n paramter: {JsonConvert.SerializeObject(paramter)}");

                },

            })
            );

            //App.GetSection<JWt>()

            services.AddSingleton<ICache, XjjxmmMemoryCache>();
            //services.AddSingleton<ILogger, Logg>();

            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    var jwtConfig = App.GetJwtConfig();

                    //var keyByteArray = System.Text.Encoding.ASCII.GetBytes(jwtConfig.Secret);
                    //var signingKey = new SymmetricSecurityKey(keyByteArray);

                    //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,//是否验证IssuerSigningKey 
                        IssuerSigningKey = jwtConfig.IssuerSigningKey,//参数配置在下边

                        ValidateIssuer = true,   //是否验证Issuer
                        ValidIssuer = jwtConfig.Issue,//发行人


                        ValidateAudience = true,//是否验证Audience 
                        ValidAudience = jwtConfig.Aud,//订阅人

                        ValidateLifetime = true,//是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 
                        //ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                        ClockSkew = jwtConfig.GetClickSkew(),

                        RequireExpirationTime = true,
                    };


                });

            //services.AddTransient<UserService>();
            //services.AddTransient<RoleService>();

            //services.AddTransient<UserRepository>();
            //services.AddTransient<RoleRepository>();
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
