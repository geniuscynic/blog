namespace XjjXmm.Authorize.Service.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleModel 
    {
        /// <summary>
        /// 唯一Id
        /// </summary>
        public string Id { get; set; }

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
