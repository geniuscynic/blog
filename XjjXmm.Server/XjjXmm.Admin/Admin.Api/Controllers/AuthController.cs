using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Api.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class AuthController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获取验证数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<CaptchaOutput> GetCaptcha()
        {
            var data = await _captcha.GetAsync();
            return data;
        }
    }
}
