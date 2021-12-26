using System.Collections.Generic;

namespace Admin.Service.Auth
{
    public class AuthUserInfoOutput
    {
        /// <summary>
        /// 用户个人信息
        /// </summary>
        public AuthUserProfileDto User { get; set; }

        /// <summary>
        /// 用户菜单
        /// </summary>
        public IEnumerable<AuthUserMenuDto> Menus { get; set; }

        /// <summary>
        /// 用户权限点
        /// </summary>
        public IEnumerable<string> Permissions { get; set; }
    }
}