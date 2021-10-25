using XjjXmm.DataBase.Utility;

namespace XjjXmmTest.entity
{


    [Table("MED_GROUP_USER")]
    public class GroupUserEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Column(ColumnName = "ID", IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 用户组ID
        /// </summary>
        [Column(ColumnName = "GROUP_ID")]
        public string GroupId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column(ColumnName = "USER_ID")]
        public string UserId { get; set; }

    }
}