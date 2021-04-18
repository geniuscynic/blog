﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DoCare.Extension.Jwt
{
    public class JwtHelper
    {
        public static string IssueJwt(TokenModelOptions options)
        {
            //string issue = ConfigurationManager.Appsetting("JWT", "Issue"); // "Issuer";
            //string aud = ConfigurationManager.Appsetting("JWT", "Aud"); // "Audience";
            //string secret = ConfigurationManager.Appsetting("JWT", "Secret"); // "ghgfopkhop gkfdopg kdfpgkdfg dfgkdfg dfgf gfdg";

            //var jwtConfig = ConfigurationManager.Appsetting("JWT");

            var jwtConfig = JwtTokenSetting.GetKey(options.AppId);


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

            var expires = jwtConfig.GetExpires();
           
            var claims = new List<Claim>
                {
                    //下边为Claim的默认配置
                new Claim(JwtRegisteredClaimNames.Jti, options.Id),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期100秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.Add(expires)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.Issue),
                new Claim(JwtRegisteredClaimNames.Aud, jwtConfig.Aud),
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
