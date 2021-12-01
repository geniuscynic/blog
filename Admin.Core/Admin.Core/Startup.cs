﻿using Admin.Core.Aop;
using Admin.Core.Auth;
using Admin.Core.Common.Auth;
using Admin.Core.Common.Cache;
using Admin.Core.Common.Configs;
using Admin.Core.Common.Consts;

//using FluentValidation;
//using FluentValidation.AspNetCore;
using Admin.Core.Common.Helpers;
using Admin.Core.Db;
using Admin.Core.Enums;
using Admin.Core.Extensions;
using Admin.Core.Filters;
using Admin.Core.Logs;
using Admin.Core.RegisterModules;
using Admin.Core.Repository;
using AspNetCoreRateLimit;
using Autofac;
using Autofac.Extras.DynamicProxy;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.Swagger;
using Yitter.IdGenerator;

namespace Admin.Core
{
    public class Startup
    {
        private static string basePath => AppContext.BaseDirectory;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;
       // private readonly ConfigHelper _configHelper;
        private readonly AppConfig _appConfig;
        private const string DefaultCorsPolicyName = "AllowPolicy";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            //_configHelper = new ConfigHelper();
            _appConfig = App.Configuration.GetSection<AppConfig>("app") ?? new AppConfig();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //雪花漂移算法
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions(1) { WorkerIdBitLength = 6 });

            //权限处理
            services.AddScoped<IPermissionHandler, PermissionHandler>();

            // ClaimType不被更改
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //用户信息
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            if (_appConfig.IdentityServer.Enable)
            {
                //is4
                services.TryAddSingleton<IUser, UserIdentiyServer>();
            }
            else
            {
                //jwt
                services.TryAddSingleton<IUser, User>();
            }

            //添加数据库
            services.AddDbAsync(_env).Wait();

            //添加IdleBus单例
            var dbConfig = App.Configuration.GetSection<DbConfig>("db");
            var timeSpan = dbConfig.IdleTime > 0 ? TimeSpan.FromMinutes(dbConfig.IdleTime) : TimeSpan.MaxValue;
            IdleBus<IFreeSql> ib = new IdleBus<IFreeSql>(timeSpan);
            services.AddSingleton(ib);
            //数据库配置
            services.AddSingleton(dbConfig);

            //应用配置
            services.AddSingleton(_appConfig);

            //上传配置
            //var uploadConfig = App.Configuration.GetSection<UploadConfig>("upload");
            //services.Configure<UploadConfig>(uploadConfig);

            #region AutoMapper 自动映射

            var serviceAssembly = Assembly.Load("Admin.Core.Service");
            services.AddAutoMapper(serviceAssembly);

            #endregion AutoMapper 自动映射

            #region Cors 跨域
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, policy =>
                {
                    var hasOrigins = _appConfig.CorUrls?.Length > 0;
                    if (hasOrigins)
                    {
                        policy.WithOrigins(_appConfig.CorUrls);
                    }
                    else
                    {
                        policy.AllowAnyOrigin();
                    }
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod();

                    if (hasOrigins)
                    {
                        policy.AllowCredentials();
                    }
                });

                //允许任何源访问Api策略，使用时在控制器或者接口上增加特性[EnableCors(AdminConsts.AllowAnyPolicyName)]
                options.AddPolicy(AdminConsts.AllowAnyPolicyName, policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

                /*
                //浏览器会发起2次请求,使用OPTIONS发起预检请求，第二次才是api异步请求
                options.AddPolicy("All", policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .SetPreflightMaxAge(new TimeSpan(0, 10, 0))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
                */
            });

            #endregion Cors 跨域

            #region 身份认证授权

            var jwtConfig = App.Configuration.GetSection<JwtConfig>("jwt");
            services.TryAddSingleton(jwtConfig);

            if (_appConfig.IdentityServer.Enable)
            {
                //is4
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = nameof(ResponseAuthenticationHandler); //401
                    options.DefaultForbidScheme = nameof(ResponseAuthenticationHandler);    //403
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = _appConfig.IdentityServer.Url;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "admin.server.api";
                })
                .AddScheme<AuthenticationSchemeOptions, ResponseAuthenticationHandler>(nameof(ResponseAuthenticationHandler), o => { });
            }
            else
            {
                //jwt
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = nameof(ResponseAuthenticationHandler); //401
                    options.DefaultForbidScheme = nameof(ResponseAuthenticationHandler);    //403
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                })
                .AddScheme<AuthenticationSchemeOptions, ResponseAuthenticationHandler>(nameof(ResponseAuthenticationHandler), o => { });
            }

            #endregion 身份认证授权

            #region Swagger Api文档
/*
            if (_env.IsDevelopment() || _appConfig.Swagger)
            {
                services.AddSwaggerGen(options =>
                {
                    typeof(ApiVersion).GetEnumNames().ToList().ForEach(version =>
                    {
                        options.SwaggerDoc(version, new OpenApiInfo
                        {
                            Version = version,
                            Title = "Admin.Core"
                        });
                        //c.OrderActionsBy(o => o.RelativePath);
                    });

                    options.ResolveConflictingActions(apiDescription => apiDescription.First());
                    options.CustomSchemaIds(x => x.FullName);

                    var xmlPath = Path.Combine(basePath, "Admin.Core.Swagger.xml");
                    options.IncludeXmlComments(xmlPath, true);

                    var xmlCommonPath = Path.Combine(basePath, "Admin.Core.Common.Swagger.xml");
                    options.IncludeXmlComments(xmlCommonPath, true);

                    var xmlModelPath = Path.Combine(basePath, "Admin.Core.Model.Swagger.xml");
                    options.IncludeXmlComments(xmlModelPath);

                    var xmlServicesPath = Path.Combine(basePath, "Admin.Core.Service.Swagger.xml");
                    options.IncludeXmlComments(xmlServicesPath);

                    #region 添加设置Token的按钮

                    if (_appConfig.IdentityServer.Enable)
                    {
                        //添加Jwt验证设置
                        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "oauth2",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                new List<string>()
                            }
                        });

                        //统一认证
                        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.OAuth2,
                            Description = "oauth2登录授权",
                            Flows = new OpenApiOAuthFlows
                            {
                                Implicit = new OpenApiOAuthFlow
                                {
                                    AuthorizationUrl = new Uri($"{_appConfig.IdentityServer.Url}/connect/authorize"),
                                    Scopes = new Dictionary<string, string>
                                    {
                                        { "admin.server.api", "admin后端api" }
                                    }
                                }
                            }
                        });
                    }
                    else
                    {
                        //添加Jwt验证设置
                        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                new List<string>()
                            }
                        });

                        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "Value: Bearer {token}",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey
                        });
                    }

                    #endregion 添加设置Token的按钮
                });
            }
*/
            #endregion Swagger Api文档

            #region 操作日志

            if (_appConfig.Log.Operation)
            {
                //services.AddSingleton<ILogHandler, LogHandler>();
                services.AddScoped<ILogHandler, LogHandler>();
            }

            #endregion 操作日志

            #region 控制器

            services.AddControllers(options =>
            {
               // options.Filters.Add<AdminExceptionFilter>();
                if (_appConfig.Log.Operation)
                {
                    options.Filters.Add<LogActionFilter>();
                }
                //禁止去除ActionAsync后缀
                options.SuppressAsyncSuffixInActionNames = false;
            })
            //.AddFluentValidation(config =>
            //{
            //    var assembly = Assembly.LoadFrom(Path.Combine(basePath, "Admin.Core.dll"));
            //    config.RegisterValidatorsFromAssembly(assembly);
            //})
            .AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //使用驼峰 首字母小写
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            })
            .AddControllersAsServices();

            #endregion 控制器

            #region 缓存

            var cacheConfig = App.Configuration.GetSection<CacheConfig>("cache");
            if (cacheConfig.Type == CacheType.Redis)
            {
                var csredis = new CSRedis.CSRedisClient(cacheConfig.Redis.ConnectionString);
                RedisHelper.Initialization(csredis);
                services.AddSingleton<ICache, RedisCache>();
            }
            else
            {
                services.AddMemoryCache();
                services.AddSingleton<ICache, MemoryCache>();
            }

            #endregion 缓存

            #region IP限流

            if (_appConfig.RateLimit)
            {
                services.AddIpRateLimit(_configuration, cacheConfig);
            }

            #endregion IP限流

            //阻止NLog接收状态消息
            services.Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = true);

            //services.Configure<IConfiguration>()
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region AutoFac IOC容器

            try
            {
                // 控制器注入
                builder.RegisterModule(new ControllerModule());

                // 单例注入
                builder.RegisterModule(new SingleInstanceModule());

                // 仓储注入
                builder.RegisterModule(new RepositoryModule());

                // 服务注入
                builder.RegisterModule(new ServiceModule(_appConfig));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.InnerException);
            }

            #endregion AutoFac IOC容器
        }

        public void Configure(IApplicationBuilder app)
        {
            #region app配置

            //IP限流
            if (_appConfig.RateLimit)
            {
                app.UseIpRateLimiting();
            }

            //异常
            app.UseExceptionHandler("/Error");

            //静态文件
            app.UseUploadConfig();

            //路由
            app.UseRouting();

            //跨域
            app.UseCors(DefaultCorsPolicyName);

            //认证
            app.UseAuthentication();

            //授权
            app.UseAuthorization();

            //配置端点
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #endregion app配置

            #region Swagger Api文档

            app.UseSwaggerMiddlewares();
            //if (_env.IsDevelopment() || _appConfig.Swagger)
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c =>
            //    {
            //        typeof(ApiVersion).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
            //        {
            //            c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Admin.Core {version}");
            //        });
            //        c.RoutePrefix = "";//直接根目录访问，如果是IIS发布可以注释该语句，并打开launchSettings.launchUrl
            //        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);//折叠Api
            //        //c.DefaultModelsExpandDepth(-1);//不显示Models
            //    });
            //}

            #endregion Swagger Api文档
        }
    }
}