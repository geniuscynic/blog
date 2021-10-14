using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.FrameWork.ToolKit.Captcha
{
    public class CaptchaResult
    {
        /// <summary>
        /// CaptchaCode
        /// </summary>
        public string CaptchaCode { get; set; }

        /// <summary>
        /// CaptchaMemoryStream
        /// </summary>
        public MemoryStream CaptchaMemoryStream { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        public string Base64 => "data:image/png;base64," + Convert.ToBase64String(CaptchaMemoryStream.GetBuffer());
    }
}
