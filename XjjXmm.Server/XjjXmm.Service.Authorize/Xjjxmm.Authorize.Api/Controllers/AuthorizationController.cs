using System;
using System.Buffers.Text;
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
        [HttpGet("code")]
        public async Task<object> GetCode()
        {
            var result = await CaptchaKit.GenerateCaptcha();
            var id = GuidKit.Get();

            //var base64 = Convert.ToBase64String(result.CaptchaMemoryStream.GetBuffer());
          
             return  new
             {
                 img = result.Base64,
                 uuid = "captch_" + id
             };
             
        }

    }
}
