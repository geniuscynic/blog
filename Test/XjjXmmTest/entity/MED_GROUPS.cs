using XjjXmm.DataBase.Utility;

namespace XjjXmmTest.entity
{
    [Table("MED_GROUPS")]
    public class GroupEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Column(ColumnName = "ID", IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Column(ColumnName = "GROUP_CODE")]
        public string GroupCode { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Column(ColumnName = "GROUP_NAME")]
        public string GroupName { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        [Column(ColumnName = "GROUP_DESC")]
        public string GroupDesc { get; set; }
    }
}