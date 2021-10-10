using DoCare.Zkzx.Core.Database.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    /// <summary>
    /// 用户,角色关联类
    /// </summary>
    [Table("xjjxmm_user_role")]
    public class UserRoleEntity
    {
        [Column(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }

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
