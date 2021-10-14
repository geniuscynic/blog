using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XjjXmm.Authorize.Service;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.ToolKit;

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
                throw BussinessException.CreateException(ExceptionCode.CustomException, "验证码不存在或者已过期");
            }

            if (authUser.Code != code)
            {
                throw BussinessException.CreateException(ExceptionCode.CustomException, "验证码错误");
            }

           var userModel = await _userService.FindUser(authUser);


           return userModel;
        }

        /// <summary>
        ///  获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("code")]
        public async Task<object> GetCode()
        {
            var result = await CaptchaKit.GenerateCaptcha();
            var id = GuidKit.Get();
            _cache.Set(id, result.CaptchaCode, TimeSpan.FromMinutes(1));
            //var base64 = Convert.ToBase64String(result.CaptchaMemoryStream.GetBuffer());

            return  new
             {
                 img = result.Base64,
                 uuid = "captch_" + id
             };
             
        }

    }
}
