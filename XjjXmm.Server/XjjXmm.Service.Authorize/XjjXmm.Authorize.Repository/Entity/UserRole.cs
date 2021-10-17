using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    /// <summary>
    /// 用户,角色关联类
    /// </summary>
    [Table("sys_users_roles")]
    public class UserRoleEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Column(ColumnName = "user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        [Column(ColumnName = "role_id")]
        public long RoleId { get; set; }

      
    }
}
