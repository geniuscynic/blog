using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Entity.RootTkey;

namespace Blog.Entity
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class User : RootEntityTkey<int>
    {
        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(Length = 6, ColumnDataType = "nvarchar")]
        public string NickName { get; set; }

        /// <summary>
        /// 最后登入时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Role> Roles { get; set; }


       
    }
}
