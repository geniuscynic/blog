

using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("sys_role")]
    public class RoleEntity : BaseEntity
    {
        [Column(IsPrimaryKey = true, ColumnName = "role_id", IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 数据权限，全部 、 本级 、 自定义
        /// </summary>
        [Column(ColumnName = "data_scope")]
        public string DataScope { get; set; }


        /// <summary>
        /// 级别，数值越小，级别越大
        /// </summary>
        [Column(ColumnName = "level")]
        public int Level { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column(ColumnName = "description")]
        public string Description { get; set; }
    }
}
