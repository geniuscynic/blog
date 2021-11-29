using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using XjjXmm.FrameWork.LogExtension;


namespace XjjXmm.FrameWork.Jwt
{
    public class JwtHelper
    {
        private static ILog<JwtHelper> logger = App.GetLog<JwtHelper>();

                        

        public static string IssueToken(JwtTokenSetting jwtConfig, TokenModelOptions options)
        {
            var expires = jwtConfig.GetExpires();

            var claims = new List<Claim>
            {
                //下边为Claim的默认配置
               // new Claim(JwtRegisteredClaimNames.Name, options.Id),
               new Claim(ClaimTypes.Name, options.Id),
                new Claim("AppId", options.AppId),
                new Claim("ClientId", options.ClientId),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                //new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期100秒，可自定义，注意JWT有自己的缓冲过期时间
                // new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.Add(expires)).ToUnixTimeSeconds()}"),
                // new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.Issue),
                //new Claim(JwtRegisteredClaimNames.Aud, jwtConfig.Aud),
                //这个Role是官方UseAuthentication要要验证的Role，我们就不用手动设置Role这个属性了
                //new Claim(ClaimTypes.Role,tokenModel.Role),
                //new Claim(ClaimTypes.Name, tokenModel.Name),
                //new Claim(ClaimTypes.NameIdentifier, tokenModel.Id.ToString()),

                
            };


            //claims.AddRange(tokenModel.Role.Select(s => new Claim(ClaimTypes.Role, s.Trim())));

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken
            (
                issuer: jwtConfig.Issue,
                audience: jwtConfig.Aud,
                expires: DateTime.Now.Add(expires),
                claims: claims,
                signingCredentials: creds

            );

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        public static TokenModelOptions DecryptToken(string jwtStr)
        {
            //var jwtConfig = JwtTokenSetting.GetKey(options.JwtKey);
            //var jwtConfig = JwtTokenSetting.GetKey(options.JwtKey);
            try
            {


                var jwtHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                if (jwtToken == null)
                {
                    return null;
                }

                return new TokenModelOptions()
                {
                    Id = jwtToken.Id,
                    AppId = jwtToken.Claims.FirstOrDefault(t => t.Type == "AppId")?.Value ?? "",
                    ClientId = jwtToken.Claims.FirstOrDefault(t => t.Type == "ClientId")?.Value ?? "",
                };

            }
            catch (Exception ex)
            {
                logger.Error( "jwt 解析错误", ex);
            }

            return null;
        }

      
        public static TokenModelOptions DecryptToken(JwtTokenSetting jwtConfig, string jwtStr)
        {
            //var jwtConfig = JwtTokenSetting.GetKey(options.JwtKey);
            //var jwtConfig = JwtTokenSetting.GetKey(options.JwtKey);
            try
            {


                var jwtHandler = new JwtSecurityTokenHandler();
                //JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                //if (jwtToken == null)
                //{
                //    return null;
                //}

                
                var keyByteArray = System.Text.Encoding.ASCII.GetBytes(jwtConfig.Secret);
                var signingKey = new SymmetricSecurityKey(keyByteArray);

                //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, //是否验证IssuerSigningKey 
                    IssuerSigningKey = signingKey, //参数配置在下边

                    ValidateIssuer = true, //是否验证Issuer
                    ValidIssuer = jwtConfig.Issue, //发行人


                    ValidateAudience = true, //是否验证Audience 
                    ValidAudience = jwtConfig.Aud, //订阅人

                    ValidateLifetime = true, //是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 
                    //ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                    ClockSkew = jwtConfig.GetClickSkew(),

                    RequireExpirationTime = true,
                };

                SecurityToken sercutityToken = null;
                
                var principal = jwtHandler.ValidateToken(jwtStr, tokenValidationParameters, out sercutityToken);
                if (principal != null && principal.Claims.Any())
                {
                    
                    return new TokenModelOptions()
                    {
                        Id = sercutityToken.Id,
                        AppId = principal.Claims.FirstOrDefault(t=>t.Type == "AppId")?.Value??"",
                        ClientId = principal.Claims.FirstOrDefault(t => t.Type == "ClientId")?.Value ?? "",
                    };
                }
            }
            catch (Exception ex)
            {
                logger.Error("jwt 解析错误", ex);
            }

            return null;

        }

        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            object id;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
                jwtToken.Payload.TryGetValue(ClaimTypes.NameIdentifier, out id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelJwt
            {
                Id = int.Parse(id.ToString()),
                 Role = role.ToString().Split(",").ToList(),
                //Role = role != null ? role.ToString() : "",
            };
            return tm;
        }


       

    }


}
