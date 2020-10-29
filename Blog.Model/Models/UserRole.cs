using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Models
{
    /// <summary>
    /// 用户,角色关联类
    /// </summary>
    public class UserRole : RootEntityTkey<int>
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }



    }
}
