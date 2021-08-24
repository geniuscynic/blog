using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using Microsoft.AspNetCore.Authorization;
using XjjXmm.Core.FrameWork.Cache;
using XjjXmm.Core.SetUp.Configuration;
using XjjXmm.Core.SetUp.Jwt;

namespace XjjXmm.Door.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICache _cache;

        public AccountController(ICache cache)
        {
            _cache = cache;
        }

        [HttpPost("login")]
        public string Login(string name, string password)
        {
            var setting = ConfigurationManager.GetSection<ClientSetting>("sdfy:authorize");
            if (setting == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }


            var option = new TokenModelOptions()
            {
                Id = "1",
                AppId = "sdf"
            };

            //做登入认证
            var jwtTokenSetting = ConfigurationManager.GetSection<JwtTokenSetting>("sdfy:JWT");
            //访问url
            var jwt = JwtHelper.IssueToken(jwtTokenSetting, option);


            _cache.Set($"authorization_{option.Id}", jwt, jwtTokenSetting.GetExpires());
            _cache.Remove($"accessToken_{option.Id}");

            return jwt;
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


            var option = JwtHelper.DecryptToken(jwtTokenSetting, authorizationCode);

            var code = _cache.Get<string>($"authorization_{option.Id}");

            if (code != authorizationCode)
            {
                return "";
            }

            _cache.Remove($"authorization_{option.Id}");

            var accessToken = JwtHelper.IssueToken(jwtTokenSetting, option);

            _cache.Set($"accessToken_{option.Id}", accessToken, jwtTokenSetting.GetExpires());

            return accessToken;
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
