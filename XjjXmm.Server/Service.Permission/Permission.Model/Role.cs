namespace Permission.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleModel 
    {
        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        //[Column(Length = 10, ColumnDataType = "nvarchar")]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = "";
    }
}
