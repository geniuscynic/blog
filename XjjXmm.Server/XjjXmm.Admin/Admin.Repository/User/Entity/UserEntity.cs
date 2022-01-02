using Admin.Repository.Role;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Admin.Repository.User
{
    /// <summary>
    /// 用户
    /// </summary>
	[SugarTable("ad_user")]
    public class UserEntity  : EntityFull
    {

        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>

        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
       
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        
        public string Avatar { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        
        public string Remark { get; set; }


          [SugarColumn(IsIgnore =true)]
        public List<RoleEntity> Roles { get; set; }
    }
}