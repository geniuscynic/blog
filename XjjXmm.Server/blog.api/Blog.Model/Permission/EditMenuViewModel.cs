using System.Collections.Generic;

namespace Blog.Model.Permission
{
    /// <summary>
    /// 添加menu
    /// </summary>
    public class EditMenuViewModel
    {
        /// <summary>
        /// 菜单 code
        /// </summary>
        public string Code { get; set; } = "";


        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Icon 名
        /// </summary>
        
        public string Icon { get; set; } = "";

        /// <summary>
        /// 路由， 导航地址
        /// </summary>
       
        public string Route { get; set; } = "";

        /// <summary>
        /// 排序
        /// </summary>
        public int SeqNum { get; set; }

        /// <summary>
        /// 父Menu Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 父 menu
        /// </summary>
        public EditMenuViewModel Parent { get; set; }

        /// <summary>
        /// 子 menu
        /// </summary>
        public List<EditMenuViewModel> ChildMenus { get; set; }
    }
}
