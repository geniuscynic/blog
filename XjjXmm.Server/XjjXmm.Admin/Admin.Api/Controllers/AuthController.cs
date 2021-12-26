using Admin.Service.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XjjXmm.FrameWork.ToolKit.Captcha;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.Jwt;

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
          
            var user = await _authService.Login(input);

            var jwtSetting = App.GetJwtConfig();

            var token = JwtHelper.IssueToken(jwtSetting, new TokenModelOptions
            {
                Id = user.Id.ToString(),
            });
            //App.Configuration.Get

            //JwtHelper.IssueToken()
            //return GetToken(res);

            return token;
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AuthUserInfoOutput> GetUserInfo()
        {
            return await _authService.GetUserInfo();
        }
    }
}
