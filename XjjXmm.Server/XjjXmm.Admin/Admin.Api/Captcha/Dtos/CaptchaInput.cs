﻿namespace Admin.Api.Captcha.Dtos
{
    public class CaptchaInput
    {
        /// <summary>
        /// 校验唯一标识
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 删除缓存
        /// </summary>
        public bool DeleteCache { get; set; } = false;

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }
}
