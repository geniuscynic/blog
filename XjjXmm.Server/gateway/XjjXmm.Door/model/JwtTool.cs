using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using XjjXmm.Core.SetUp.Configuration;
using XjjXmm.Core.SetUp.Jwt;

namespace XjjXmm.Door.model
{
    public class JwtTool
    {
        private static ILogger logger = Log.Logger;

        public static TokenModelOptions DecryptAndValidationToken(string authorizationCode)
        {
            //var jwtConfig = JwtTokenSetting.GetKey(options.JwtKey);
            //var jwtConfig = JwtTokenSetting.GetKey(options.JwtKey);
            //try
            //{
                var option = JwtHelper.DecryptToken(authorizationCode);
                if (option == null)
                {
                    throw BussinessException.CreateException(ExceptionCode.CustomException, "非法的token");
                }


                var jwtTokenSetting = ConfigurationManager.GetSection<JwtTokenSetting>($"{option.ClientId}:JWT");
                if (jwtTokenSetting == null)
                {
                    throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
                }

                var setting = ConfigurationManager.GetSection<ClientSetting>($"{option.ClientId}:authorize");
                if (setting == null)
                {
                    throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
                }


                var option1 = JwtHelper.DecryptToken(jwtTokenSetting, authorizationCode);
                if (option1 == null)
                {
                    throw BussinessException.CreateException(ExceptionCode.CustomException, "非法的token");
                }

                return option1;


           // }
            //catch (Exception ex)
           // {
            //    logger.Error(ex, "jwt 解析错误");
           // }

           // return null;

        }
    }
}
