﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Entity.RootTkey;

namespace Blog.Entity
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

        /// <summary>
        /// 对应的用户
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public User User { get; set; }

        /// <summary>
        /// 对应的角色
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Role Role {get;set;}
}
}
