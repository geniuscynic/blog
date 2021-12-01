using System;
using System.Linq;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XjjXmm.FrameWork.Configuration;
using XjjXmm.FrameWork.Jwt;
using XjjXmm.FrameWork.LogExtension;

namespace XjjXmm.FrameWork
{
    public static class App
    {
        //public static void Start(string environmentName = "")
        //{
        //    Configuration.Scan(environmentName);
        //}

        /// <summary>
        /// 配置类
        /// </summary>
       
        public static readonly XjjXmmConfiguration Configuration = new XjjXmmConfiguration();


        /// <summary>
        /// 服务提供其
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }

        public static ILog<DefaultLogger> Logger => GetLog<DefaultLogger>();

        public static ILog<T> GetLog<T>()
        {
            return ServiceProvider.GetService<ILog<T>>();
        }

        public static string? UserId {
            get
            {
                var context = ServiceProvider.GetService<IHttpContextAccessor>();
                return context?.HttpContext?.User?.Identity?.Name;
            }
        } 

        public static JwtTokenSetting GetJwtConfig(string key = "JWT")
        {
            return JwtTokenSetting.GetKey(key);
        }

        
    }
}
