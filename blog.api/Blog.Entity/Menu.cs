using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Entity.RootTkey;

namespace Blog.Entity
{
    /// <summary>
    /// 菜单配置
    /// </summary>
    public class Menu : RootEntityTkey<int>

    {
        /// <summary>
        /// 菜单 code
        /// </summary>
        [SugarColumn(Length = 10, ColumnDataType = "nvarchar")]
        public string Code { get; set; } = "";


        /// <summary>
        /// 菜单名
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Name { get; set; }


        /// <summary>
        /// Icon 名
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Icon { get; set; } = "";

        /// <summary>
        /// 路由， 导航地址
        /// </summary>
        [SugarColumn(Length = 200)]
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
        [SugarColumn(IsIgnore = true)]
        public Menu Parent { get; set; }

        /// <summary>
        /// 子 menu
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Menu> ChildMenus { get; set; }


        /// <summary>
        /// 子 menu
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Button> Buttons { get; set; }
    }
}
