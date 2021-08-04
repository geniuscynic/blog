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
        [HttpPost]
        public string Login(string name, string password, string clientId)
        {
            var setting = ConfigurationManager.GetSection<ClientSetting>(clientId);
            if (setting == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }

            //访问url

            return JwtHelper.IssueJwt(new TokenModelOptions()
            {
                Id = "1",
                AppId = clientId,
                JwtKey = "JWT"
            });
        }

        public string GetAccessToken(string authorizationCode, string clientSecret)
        {
            var clientId = "";

            var setting = ConfigurationManager.GetSection<ClientSetting>(clientId);
            if (setting == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }
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
