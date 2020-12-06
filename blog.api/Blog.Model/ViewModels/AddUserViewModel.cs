using System;
using System.Collections.Generic;

namespace Blog.Core.ViewModels
{
    /// <summary>
    /// 添加用户的view model
    /// </summary>
    public class AddUserViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        //todo 要做权限判断，只有管理员才能设置初始role
        /// <summary>
        /// 所属角色
        /// </summary>
        public List<int> Roles { get; set; }
    }
}