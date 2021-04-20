using XjjXmm.Core.Database.Utility;

namespace Permission.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role 
    {
        [Column( IsPrimaryKey = true)]
        public int Id { get; set; }

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
