using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using Microsoft.AspNetCore.Authorization;
using XjjXmm.Core.SetUp.Configuration;
using XjjXmm.Core.SetUp.Jwt;

namespace XjjXmm.Door.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("login")]
        public string Login(string name, string password)
        {
            var setting = ConfigurationManager.GetSection<ClientSetting>("sdfy:authorize");
            if (setting == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }

            //做登入认证
            var jwtTokenSetting = ConfigurationManager.GetSection<JwtTokenSetting>("sdfy:JWT");
            //访问url
            return JwtHelper.IssueToken(jwtTokenSetting, new TokenModelOptions()
            {
                Id = "1",
                AppId = "sdf"
            });
        }

        [HttpPost("GetAccessToken")]
        public string GetAccessToken(string authorizationCode)
        {
            //做登入认证
            var jwtTokenSetting = ConfigurationManager.GetSection<JwtTokenSetting>("sdfy:JWT");

            var setting = ConfigurationManager.GetSection<ClientSetting>("sdfy:authorize");
            if (setting == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }



            var options = JwtHelper.DecryptToken(jwtTokenSetting, authorizationCode);

            return JwtHelper.IssueToken(jwtTokenSetting, new TokenModelOptions()
            {
                Id = "1",
                AppId = "sdfy:JWT"
            });
        }


        [HttpGet("test1")]
        public string Test1()
        {
            return "test1";
        }

        [HttpGet("test2")]
        [Authorize]
        public string Test2()
        {
            return "test2";
        }
    }
}
