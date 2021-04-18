using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.Framework.Configuration;
using XjjXmm.Framework.Tool;

namespace XjjXmm.Framework.Jwt
{
    public class JwtTokenSetting
    {
        public string Issue { get; set; } = "xjjxmm Issue";
        
        public string Aud { get; set; } = "xjjxmm Aud";

        public string Secret { get; set; } = "xjjxmm issue aud secret ";

        public string ClockSkew { get; set; } = "1m";

        public string Expires { get; set; } = "2h";


        public static JwtTokenSetting GetKey(string key)
        {
            var jwtConfig = ConfigurationManager.GetSection<JwtTokenSetting>("JWT");

            if (jwtConfig != null)
            {
               jwtConfig = new JwtTokenSetting();
            }

            return jwtConfig;
        }

        public TimeSpan GetClickSkew()
        {
            return ClockSkew.ToTimeSpan(TimeSpan.FromSeconds(0));
        }

        public TimeSpan GetExpires()
        {
            return Expires.ToTimeSpan(TimeSpan.FromSeconds(0));
        }
    }
}
