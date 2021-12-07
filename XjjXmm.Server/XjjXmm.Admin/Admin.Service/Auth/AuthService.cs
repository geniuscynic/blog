using System;
using System.Threading.Tasks;
using Admin.Service.Auth.Input;
using Admin.Service.Auth.Output;
using Admin.Service.Common;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.ToolKit;

namespace Admin.Service.Auth
{
    [Injection]
    public class AuthService : IAuthService
    {
        private readonly ICache _cache;


        public AuthService( ICache cache
          
        )
        {
            _cache = cache;
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
            return null;
        }
    }
}