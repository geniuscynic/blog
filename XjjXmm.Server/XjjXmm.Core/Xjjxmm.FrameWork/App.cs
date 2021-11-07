using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XjjXmm.FrameWork.Jwt;

namespace XjjXmm.FrameWork
{
    public static class App
    {
        /// <summary>
        /// 配置类
        /// </summary>
        public static IConfiguration Configuration { get; set; }


        /// <summary>
        /// 服务提供其
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }


        public static JwtTokenSetting GetJwtConfig(string key = "JWT")
        {
            return JwtTokenSetting.GetKey(key);
        }


        public static string GetConfig(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, $"{ToKey(sections)} 不存在", null);
            }

            return "";
        }

        public static T GetSection<T>(params string[] keys) where T : class
        {
            try
            {
                if (keys.Any())
                {
                    return Configuration.GetSection(ToKey(keys)).Get<T>();
                }



            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, $"{ToKey(keys)}: 不存在", null);
            }

            return null;
        }

        private static string ToKey(params string[] keys)
        {
            return string.Join(":", keys);
        }
    }
}
