using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace XjjXmm.Framework.Configuration
{
    public class ConfigurationManager
    {
        static IConfiguration Configuration { get; set; }

        public static ILogger<ConfigurationManager> Logger { get; set; }

        public ConfigurationManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public static string Appsetting(params string[] sections)
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
                Logger?.LogError(ex, $"{ToKey(sections)} 不存在", null);
            }

            return "";
        }

        public static T GetSection<T>(params string[] keys) where T:class
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
                Logger?.LogError(ex, $"{ToKey(keys)}: 不存在", null);
            }

            return null;
        }

        private static string ToKey(params string[] keys)
        {
            return string.Join(":", keys);
        }
        
    }
}
