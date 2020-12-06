namespace Blog.Core.ViewModels
{
    /// <summary>
    /// 添加角色模型
    /// </summary>
    public class AddRoleViewModel
    {
        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = "";
    }
}