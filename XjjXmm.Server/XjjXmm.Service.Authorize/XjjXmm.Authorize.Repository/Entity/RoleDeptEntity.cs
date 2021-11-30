using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    /// <summary>
    /// 用户,角色关联类
    /// </summary>
    [Table("sys_roles_depts")]
    public class RoleDeptEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Column(ColumnName = "role_id")]
        public long RoleId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        [Column(ColumnName = "dept_id")]
        public long DeptId { get; set; }

      
    }
}
