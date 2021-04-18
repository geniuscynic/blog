using System.Collections.Generic;

namespace DoCare.Extension.Jwt
{

    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJwt
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public List<string> Role { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

    }
}
