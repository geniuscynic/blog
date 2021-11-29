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
                //��֤middleware����
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
                        ValidateIssuerSigningKey = true,//�Ƿ���֤IssuerSigningKey 
                        IssuerSigningKey = jwtConfig.IssuerSigningKey,//�����������±�

                        ValidateIssuer = true,   //�Ƿ���֤Issuer
                        ValidIssuer = jwtConfig.Issue,//������


                        ValidateAudience = true,//�Ƿ���֤Audience 
                        ValidAudience = jwtConfig.Aud,//������

                        ValidateLifetime = true,//�Ƿ���֤��ʱ  ������exp��nbfʱ��Ч ͬʱ����ClockSkew 
                        //ClockSkew = TimeSpan.Zero,//����ǻ������ʱ�䣬Ҳ����˵����ʹ���������˹���ʱ�䣬����ҲҪ���ǽ�ȥ������ʱ��+���壬Ĭ�Ϻ�����7���ӣ������ֱ������Ϊ0
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

            //��֤
            app.UseAuthentication();

            //��Ȩ
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
