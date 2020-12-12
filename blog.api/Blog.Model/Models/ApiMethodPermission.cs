using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Models
{
    /// <summary>
    /// 按钮配置
    /// </summary>
    public class ApiMethodPermission : RootEntityTkey<int>
    {

        /// <summary>
        /// 菜单 ID
        /// </summary>
        public int ApiId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 对应的菜单
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<ApiMethod> ApiMethods { get; set; }

        /// <summary>
        /// 对应的角色
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Role> Roles { get; set; }



    }
}
