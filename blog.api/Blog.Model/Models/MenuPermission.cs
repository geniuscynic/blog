using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Models
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    public class MenuPermission : RootEntityTkey<int>
    {
        /// <summary>
        /// 菜单 ID
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 对应的菜单
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Menu> Menus { get; set; }

        /// <summary>
        /// 对应的角色
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Role> Roles { get; set; }
    }
}
