using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XjjXmm.Authorize.Service;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.Jwt;
using XjjXmm.FrameWork.ToolKit;
using XjjXmm.FrameWork.ToolKit.DataEncryption.Extensions;

namespace XjjXmm.Authorize.Api.Controllers
{
    /// <summary>
    /// 系统：系统授权接口
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthorizationController
    {
        private readonly ICache _cache;
        private readonly UserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="userService"></param>
        public AuthorizationController(ICache cache, UserService userService)
        {
            _cache = cache;
            _userService = userService;
        }

        /// <summary>
        /// 登录授权
        /// </summary>
        /// <param name="authUser"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<object> Login(AuthUserDto authUser)
        {
            var code = _cache.Get<string>(authUser.UUID);

            if (string.IsNullOrEmpty(code))
            {
               // throw BussinessException.CreateException(ExceptionCode.CustomException, "验证码不存在或者已过期");
            }

            if (!string.Equals(authUser.Code, code, StringComparison.CurrentCultureIgnoreCase))
            {
               // throw BussinessException.CreateException(ExceptionCode.CustomException, "验证码错误");
            }

            _cache.Remove(authUser.UUID);

            var rsaKey = App.GetConfig("rsa:private_key");
            var password = authUser.Password.ToRSADecrypt(rsaKey);
            authUser.Password = password;

            var userModel = await _userService.FindUser(authUser);

            //做登入认证
            var jwtTokenSetting = App.GetSection<JwtTokenSetting>("JWT");
            var jwtStr = JwtHelper.IssueToken(jwtTokenSetting, new TokenModelOptions()
            {
                AppId = "xjjxmm",
                ClientId = "admin",
                Id = userModel.Id.ToString()
            });

            return new
            {
                token = $"Bearer {jwtStr}",
                user = userModel
            };
        }

        /// <summary>
        ///  获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("code")]
        public async Task<object> GetCode()
        {
            var result = await CaptchaKit.GenerateCaptcha();
            var id = "captch_" + GuidKit.Get();
            _cache.Set(id, result.CaptchaCode, TimeSpan.FromMinutes(1));
            //var base64 = Convert.ToBase64String(result.CaptchaMemoryStream.GetBuffer());

            return new
            {
                img = result.Base64,
                uuid = id
            };

        }

    }
}
