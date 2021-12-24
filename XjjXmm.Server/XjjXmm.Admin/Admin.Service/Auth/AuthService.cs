using System;
using System.Threading.Tasks;
using Admin.Repository.User;
using Admin.Service.Auth.Input;
using Admin.Service.Auth.Output;
using Admin.Service.Common;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;
using XjjXmm.FrameWork.ToolKit;
using XjjXmm.FrameWork.ToolKit.Captcha;

namespace Admin.Service.Auth
{
    [Injection]
    public class AuthService : IAuthService
    {
        private readonly ICache _cache;
        private readonly ICaptcha _captcha;
        private readonly IUserRepository _userRepository;

        public AuthService( ICache cache ,
            ICaptcha captcha ,
            IUserRepository userRepository

        )
        {
            _cache = cache;
            _captcha = captcha;
            _userRepository = userRepository;
        }

        public Task<object> GetPassWordEncryptKey()
        {
            //写入Redis
            var guid = GuidKit.Get();
            var key = string.Format(CacheKey.PassWordEncryptKey, guid);
            var encyptKey = StringKit.GenerateRandom(8);
            _cache.Set(key, encyptKey, TimeSpan.FromMinutes(5));
            var data = new { key = guid, encyptKey };

            return Task.FromResult<object>(data);
        }

        public async Task<AuthLoginOutput> Login(AuthLoginInput input)
        {
            var isOK = await _captcha.Check(input.Captcha);

            if (!isOK)
            {
               // throw new BussinessException(StatusCodes.Status999Falid, "验证码输入有误");
            }

            if (!input.PasswordKey.IsNullOrEmpty())
            {
                var passwordEncryptKey = string.Format(CacheKey.PassWordEncryptKey, input.PasswordKey);
                var secretKey = _cache.Get<string>(passwordEncryptKey);
                if (!secretKey.IsNullOrEmpty())
                {
                    
                    input.Password = Encryptions.DesDecrypt(secretKey, input.Password);
                    //await Cache.DelAsync(passwordEncryptKey);
                    _cache.Remove(passwordEncryptKey);
                }
                else
                {
                    // return ResponseOutput.NotOk("解密失败！", 1);
                    throw new BussinessException(StatusCodes.Status999Falid, "解密失败！");
                }
            }

            var user = await _userRepository.GetFirst(t=>t.UserName == input.UserName);

            if (!(user?.Id > 0))
            {
              
                throw new BussinessException(StatusCodes.Status999Falid, "账号输入有误!");
            }

            var password = Encryptions.MD5(input.Password);
            if (user.Password != password)
            {
                throw new BussinessException(StatusCodes.Status999Falid, "密码输入有误!");
            }

            return user.MapTo<UserEntity, AuthLoginOutput>();
        }
    }
}