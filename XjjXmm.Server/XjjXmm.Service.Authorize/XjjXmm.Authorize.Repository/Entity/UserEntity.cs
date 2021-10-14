﻿using System;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    /// <summary>
    /// 用户类
    /// </summary>
    [Table("sys_user")]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        ///  ID
        /// </summary>
        [Column(ColumnName = "user_id", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Column(ColumnName = "username")]
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Column(ColumnName = "nickName")]
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column(ColumnName = "email")]
        public string Email { get; set; }


        /// <summary>
        /// 电话号码
        /// </summary>
        [Column(ColumnName = "phone")]
        public string Phone { get; set; }


        /// <summary>
        /// 用户性别
        /// </summary>
        [Column(ColumnName = "gender")]
        public string Gender { get; set; }

        /// <summary>
        /// 头像真实名称
        /// </summary>
        [Column(ColumnName = "avatarName")]
        public string AvatarName { get; set; }

        /// <summary>
        /// 头像存储的路径
        /// </summary>
        [Column(ColumnName = "avatarPath")]
        public string AvatarPath { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Column(ColumnName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column(ColumnName = "enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 是否为admin账号
        /// </summary>
        [Column(ColumnName = "isAdmin")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 最后修改密码的时间
        /// </summary>
        [Column(ColumnName = "pwd_reset_time")]
        public DateTime PwdResetTime { get; set; }
    }
}
