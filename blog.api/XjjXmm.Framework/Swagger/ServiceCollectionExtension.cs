using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using XjjXmm.Framework.Filter;
using XjjXmm.Framework.Permission;

namespace XjjXmm.Framework.Swagger
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services,
            SwaggerConfig swaggerConfig = null)
        {
            swaggerConfig ??= new SwaggerConfig();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = swaggerConfig.Version,
                    Title = swaggerConfig.Title,
                    Description = swaggerConfig.Description,
                    Contact = new OpenApiContact
                    {
                        Name = swaggerConfig.ContactName, Email = swaggerConfig.ContactEmail,
                        Url = new Uri(swaggerConfig.ContactUrl)
                    },
                    License = new OpenApiLicense
                        {Name = swaggerConfig.LicenseName, Url = new Uri(swaggerConfig.LicenseUrl)}

                });

                c.OrderActionsBy(o => o.RelativePath);

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                /* var xmlPath = Path.Combine(AppContext.BaseDirectory, "Blog.API.xml");//这个就是刚刚配置的xml文件名
                 c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
 
                 xmlPath = Path.Combine(AppContext.BaseDirectory, "Blog.Model.xml");//这个就是刚刚配置的xml文件名
                 c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改*/

                var comments = Directory.GetFiles(AppContext.BaseDirectory, "*.Swagger.xml");
                if (comments != null)
                {
                    foreach (var comment in comments)
                    {
                        c.IncludeXmlComments(comment, true);
                    }
                }

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });

            return services;

        }


        public static IServiceCollection AddPermissionSetup(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, CustomPermissionHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("xjjXmmPermission", policy =>
                    policy.Requirements.Add(new CustomRequirement()));
            });

            return services;
        }
    }
}
