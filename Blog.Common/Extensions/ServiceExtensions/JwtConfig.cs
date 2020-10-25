using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Common.Extensions.ServiceExtensions
{
    public static class JwtConfig
    {
        public static void AddJwtSetup(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ID", policy => policy.RequireRole("ID").Build());
                options.AddPolicy("IA", policy => policy.RequireRole("IA").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("IA", "ID"));
            });

            string issue = AppSettingHelper.App("JWT", "Issue"); // "Issuer";
            string aud = AppSettingHelper.App("JWT", "Aud"); // "Audience";
            string secret = AppSettingHelper.App("JWT", "Secret"); // "ghgfopkhop gkfdopg kdfpgkdfg dfgkdfg dfgf gfdg";



            

            services.AddAuthentication(x =>
            {
                //看这个单词熟悉么？没错，就是上边错误里的那个。
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })// 也可以直接写字符串，AddAuthentication("Bearer")
            .AddJwtBearer(o =>
            {
                var keyByteArray = System.Text.Encoding.ASCII.GetBytes(secret);
                var signingKey = new SymmetricSecurityKey(keyByteArray);

                //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                o.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,   //是否验证Issuer
                    ValidateAudience = true,//是否验证Audience 
                    ValidateIssuerSigningKey = true,//是否验证IssuerSigningKey 
                    ValidateLifetime = true,//是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 

                    ValidIssuer = issue,//发行人
                    ValidAudience = aud,//订阅人
                    IssuerSigningKey = signingKey,//参数配置在下边


                    //ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                    ClockSkew = TimeSpan.FromSeconds(30) ,
                    RequireExpirationTime = true,
                };

            });
        }
    }
}
