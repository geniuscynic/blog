using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.FrameWork.ToolKit.Captcha
{
    public class CaptchaInput
    {
        /// <summary>
        /// 校验唯一标识
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }

    public class CaptchaOutput
    {
        /// <summary>
        /// 校验唯一标识
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
