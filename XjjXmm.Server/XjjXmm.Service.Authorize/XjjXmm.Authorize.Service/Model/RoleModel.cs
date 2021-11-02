namespace XjjXmm.Authorize.Service.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class AddRoleModel
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

    /// <summary>
    /// 角色
    /// </summary>
    public class RoleModel 
    {
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据权限，全部 、 本级 、 自定义
        /// </summary>
        public string DataScope { get; set; }


        /// <summary>
        /// 级别，数值越小，级别越大
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        //public string Description { get; set; }
    }
}
