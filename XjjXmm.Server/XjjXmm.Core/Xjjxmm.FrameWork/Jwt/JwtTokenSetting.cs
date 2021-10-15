using System;

namespace XjjXmm.FrameWork.Jwt
{
    public class JwtTokenSetting
    {
        /*
         *
         *   iss (issuer)：签发人
         *   exp (expiration time)：过期时间
         *   sub (subject)：主题
         *   aud (audience)：受众
         *   nbf (Not Before)：生效时间
         *   iat (Issued At)：签发时间
         *   jti (JWT ID)：编号
         *
         *
         */
        public string Issue { get; set; } = "xjjxmm Issue";
        
        public string Aud { get; set; } = "xjjxmm Aud";

        public string Secret { get; set; } = "xjjxmm issue aud secret ";

        public string ClockSkew { get; set; } = "1m";

        public string Expires { get; set; } = "2h";


        public static JwtTokenSetting GetKey(string key)
        {
            var jwtConfig = ConfigurationManager.GetSection<JwtTokenSetting>(key);

            if (jwtConfig == null)
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
