using System;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Tools.Captcha
{
    /// <summary>
    /// 验证接口
    /// </summary>
    [ProcessLog]
    public interface ICaptcha
    {
        /// <summary>
        /// 获得验证数据
        /// </summary>
        /// <returns></returns>
        Task<CaptchaOutput> GetAsync();

        /// <summary>
        /// 检查验证数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CheckAsync(CaptchaInput input);
    }
}