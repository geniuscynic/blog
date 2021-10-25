using XjjXmm.DataBase.Utility;

namespace XjjXmmTest.blog
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("Role")]
    public class BlogRoleEntity 
    {
        [Column( IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// code
        /// </summary>
        //[Column(Length = 10, ColumnDataType = "nvarchar")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        //[Column(Length = 10, ColumnDataType = "nvarchar")]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        //[Column(Length = 50, ColumnDataType = "nvarchar")]
        public string Description { get; set; } = "";
    }
}
