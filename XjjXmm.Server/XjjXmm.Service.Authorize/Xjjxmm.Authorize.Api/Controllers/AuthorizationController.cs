using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        ///  获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost("code")]
        public async Task<FileStreamResult> GetCode()
        {
            var result = await CaptchaKit.GenerateCaptcha();


             return new FileStreamResult(result.CaptchaMemoryStream, "image/png");
        }

    }
}
