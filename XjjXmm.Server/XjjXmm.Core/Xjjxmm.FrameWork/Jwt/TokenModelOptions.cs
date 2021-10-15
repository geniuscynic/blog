namespace XjjXmm.FrameWork.Jwt
{

    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelOptions
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }


        public string ClientId { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string AppId { get; set; } = "";

        //public string JwtKey { get; set; }


    }
}
