using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Blog.Common
{
    public class AppSettingHelper
    {
        static IConfiguration Configuration { get; set; }

        public AppSettingHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public static string App(params string[] sections)
        {
            try
            {

                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception) { }

            return "";
        }
    }
}
