namespace Admin.Service.Api
{
    /// <summary>
    /// 修改
    /// </summary>
    public partial class ApiUpdateInput : ApiAddInput
    {
        /// <summary>
        /// 接口Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public long Version { get; set; }
    }
}