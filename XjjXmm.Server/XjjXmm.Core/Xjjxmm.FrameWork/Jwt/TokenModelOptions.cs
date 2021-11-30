using System.Collections.Generic;

namespace XjjXmm.FrameWork.Jwt
{

    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelOptions
    {
        /// <summary>
        /// 用户ID Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public List<string> Roles { get; set; } 


        public string ClientId { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string AppId { get; set; } = "";

        //public string JwtKey { get; set; }


    }
}
