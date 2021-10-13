using System.Threading.Tasks;
using XjjXmm.FrameWork.ToolKit.Captcha;

namespace XjjXmm.FrameWork.ToolKit
{
    public class CaptchaKit
    {
        public static async Task<CaptchaResult> GenerateCaptcha()
        {
            var captcha = new DefaultCaptcha();

            var code = await captcha.GenerateRandomCaptchaAsync();

            var result = await captcha.GenerateCaptchaImageAsync(code);

            return result;
        }
    }
}
