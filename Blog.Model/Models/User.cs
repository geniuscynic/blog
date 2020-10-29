﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Models
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class User : RootEntityTkey<int>
    {
        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(Length = 20)]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(Length = 20)]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(Length = 6)]
        public string NickName { get; set; }

        /// <summary>
        /// 最后登入时间
        /// </summary>
        public DateTime LoginTime { get; set; }



    }
}
