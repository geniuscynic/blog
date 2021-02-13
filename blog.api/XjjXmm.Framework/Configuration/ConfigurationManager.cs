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
                Logger?.LogError(ex, $"{string.Join(":", sections)} 不存在", null);
            }

            return "";
        }

        public static T GetSection<T>(string key) where T:class
        {
            try
            {

                return Configuration.GetSection(key).Get<T>();


            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, $"{key}: 不存在", null);
            }

            return null;
        }
        
    }
}
