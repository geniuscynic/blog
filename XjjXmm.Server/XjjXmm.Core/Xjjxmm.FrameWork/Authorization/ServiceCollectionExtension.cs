using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace XjjXmm.FrameWork.Authorization
{
    internal static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigAuthentication(this IServiceCollection services)
        {
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

            return services;
        }

        public static IServiceCollection ConfigAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(option =>
            {
                option.AddPolicy("XjjXmmPolicy", configurePolicy =>
                {
                    //configurePolicy.RequireRole("admin", "test");
                    //configurePolicy.RequireUserName("aa");
                    configurePolicy.AddRequirements(new XjjXmmPermissionAuthorizationRequirement());

                });
            });

            return services;
        }

    }
}
