using SqlSugar;
using System;
using System.Collections.Generic;

namespace Admin.Repository.User
{
    /// <summary>
    /// 用户
    /// </summary>
	[SugarTable("ad_user")]
    public class UserEntity 
    {

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
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


        /// <summary>
        /// 版本
        /// </summary>
      
        public long Version { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 创建者Id
        /// </summary>
        public long? CreatedUserId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatedUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 修改者Id
        /// </summary>
        public long? ModifiedUserId { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string ModifiedUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }
    }
}