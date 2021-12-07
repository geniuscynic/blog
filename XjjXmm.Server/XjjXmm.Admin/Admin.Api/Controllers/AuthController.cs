using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Service.Auth;
using Admin.Service.Auth.Input;
using XjjXmm.FrameWork.ToolKit.Captcha;

namespace Admin.Api.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly ICaptcha _captcha;
        private readonly IAuthService _authService;

        public AuthController(ICaptcha captcha, IAuthService authService)
        {
            _captcha = captcha;
            _authService = authService;
        }

       


        /// <summary>
        /// 获取验证数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<CaptchaOutput> GetCaptcha()
        {
            var data = await _captcha.Get();
            return data;
        }

        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetPassWordEncryptKey()
        {
            return await _authService.GetPassWordEncryptKey();
        }

        /// <summary>
        /// 用户登录
        /// 根据登录信息生成Token
        /// </summary>
        /// <param name="input">登录信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Login(AuthLoginInput input)
        {
          
            //var res = await _authService.LoginAsync(input);
            
            //return GetToken(res);

            return null;
        }
    }
}
