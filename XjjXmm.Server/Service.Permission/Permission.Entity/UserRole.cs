
using System;
using System.Collections.Generic;
using System.Text;
using DoCare.Zkzx.Core.Database.Utility;



namespace Permission.Entity
{
    /// <summary>
    /// 用户,角色关联类
    /// </summary>
    public class UserRoleEntity
    {
        [Column(IsPrimaryKey = true)]
        public string Id { get; set; }

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
        //[SugarColumn(IsIgnore = true)]
        // public UserEntity User { get; set; }

        /// <summary>
        /// 对应的角色
        /// </summary>
        // [SugarColumn(IsIgnore = true)]
        // public RoleEntity Role {get;set;}
    }
}
