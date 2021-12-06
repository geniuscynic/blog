using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.Common;

namespace XjjXmm.FrameWork.ToolKit.Captcha
{
    public class DefaultCaptcha : ICaptcha
    {
        private readonly ICache _cache;
        private const string Letters = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";
        private const string CaptchaKey = "Captcha_";
        public DefaultCaptcha(ICache cache)
        {
            _cache = cache;
        }

        private Task<CaptchaOutput> GenerateCaptchaImageAsync(string captchaCode, int width = 0, int height = 30)
        {
            
            //验证码颜色集合
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

            //验证码字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };

            //定义图像的大小，生成图像的实例
            var image = new Bitmap(width == 0 ? captchaCode.Length * 25 : width, height);

            var g = Graphics.FromImage(image);

            //背景设为白色
            g.Clear(Color.White);

            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                var x = random.Next(image.Width);
                var y = random.Next(image.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }

            //验证码绘制在g中
            for (var i = 0; i < captchaCode.Length; i++)
            {
                //随机颜色索引值
                var cindex = random.Next(c.Length);

                //随机字体索引值
                var findex = random.Next(fonts.Length);

                //字体
                var f = new Font(fonts[findex], 15, FontStyle.Bold);

                //颜色  
                Brush b = new SolidBrush(c[cindex]);

                var ii = 4;
                if ((i + 1) % 2 == 0)
                    ii = 2;

                //绘制一个验证字符  
                g.DrawString(captchaCode.Substring(i, 1), f, b, 17 + (i * 17), ii);
            }

            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            ms.Position = 0;

            g.Dispose();
            image.Dispose();

            var id = GuidKit.Get();
            _cache.Set($"{CaptchaKey}id", captchaCode);
            return Task.FromResult(new CaptchaOutput()
            {
               Token = id,                  
               Data = "data:image/png;base64," + Convert.ToBase64String(ms.GetBuffer())
            });
        }

        private Task<string> GenerateRandomCaptchaAsync(int codeLength = 4)
        {
            var array = Letters.Split(new[] { ',' });

            var random = new Random();

            var temp = -1;

            var captcheCode = string.Empty;

            for (int i = 0; i < codeLength; i++)
            {
                if (temp != -1)
                    random = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));

                var index = random.Next(array.Length);

                if (temp != -1 && temp == index)
                    return GenerateRandomCaptchaAsync(codeLength);

                temp = index;

                captcheCode += array[index];
            }

            return Task.FromResult(captcheCode);
        }

        public async Task<CaptchaOutput> Get()
        {
            var captcha = await GenerateRandomCaptchaAsync();

            return await GenerateCaptchaImageAsync(captcha);
        }

        public Task<bool> Check(CaptchaInput input)
        {
            //_cache.Set($"{CaptchaKey}id", captchaCode);
            var val = _cache.Get<string>($"{CaptchaKey}{input.Token}");
            if (string.IsNullOrEmpty(val))
            {
                throw new BussinessException(StatusCodes.Status999Falid, "验证码已失效,请刷新重试");
            }

            return Task.FromResult<bool>(val == input.Data);
        }
    }
}
