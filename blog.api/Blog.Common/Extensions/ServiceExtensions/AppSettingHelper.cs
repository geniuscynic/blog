using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Common.Extensions.ServiceExtensions
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
