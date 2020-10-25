using Blog.Common.Extensions.ServiceExtensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.API
{
    public class JwtHelper
    {
        public static string IssueJwt(TokenModelJwt tokenModel)
        {
            string issue = AppSettingHelper.App("JWT", "Issue"); // "Issuer";
            string aud = AppSettingHelper.App("JWT", "Aud"); // "Audience";
            string secret = AppSettingHelper.App("JWT", "Secret"); // "ghgfopkhop gkfdopg kdfpgkdfg dfgkdfg dfgf gfdg";


            //var jwt1 = ;

            //var claims = new List<Claim>
            //{
            //    new Claim("jti", tokenModel.Uid.ToString()),
            //    new Claim("iat", $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
            //    new Claim("nbf",$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
            //    //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
            //    new Claim ("exp",$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
            //    new Claim("iss",issue ),
            //    new Claim("aud",aud),
            //};

            var claims = new List<Claim>
                {
                    //下边为Claim的默认配置
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期100秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(100)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,"Blog.Core"),
                new Claim(JwtRegisteredClaimNames.Aud,"wr"),
                //这个Role是官方UseAuthentication要要验证的Role，我们就不用手动设置Role这个属性了
                new Claim(ClaimTypes.Role,tokenModel.Role),
               };


            claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s.Trim())));

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken
            (
                issuer: issue,
                claims: claims,
                signingCredentials: creds

            );

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }


        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelJwt
            {
                Uid = int.Parse(jwtToken.Id),
                Role = role != null ? role.ToString() : "",
            };
            return tm;
        }
    }


    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJwt
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }

    }
}
