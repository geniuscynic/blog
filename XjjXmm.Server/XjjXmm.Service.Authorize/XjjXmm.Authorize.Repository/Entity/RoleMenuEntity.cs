

using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("sys_roles_menus")]
    public class RoleMenuEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Column(ColumnName = "menu_id")]
        public long MenuId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        [Column(ColumnName = "role_id")]
        public long RoleId { get; set; }
    }
}
