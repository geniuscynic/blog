using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace XjjXmm.FrameWork
{
    public static class App
    {
        public static IConfiguration Configuration { get; set; }

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
