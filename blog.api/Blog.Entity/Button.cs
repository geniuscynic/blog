using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Entity.RootTkey;

namespace Blog.Entity
{
    /// <summary>
    /// 按钮配置
    /// </summary>
    public class Button : RootEntityTkey<int>
    {
        /// <summary>
        /// 按钮 code
        /// </summary>
        [SugarColumn(Length = 20,ColumnDataType = "nvarchar")]
        public string Code { get; set; } = "";


        /// <summary>
        /// 按钮名
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Name { get; set; }


        /// <summary>
        /// 按钮名
        /// </summary>
        public int MenuId { get; set; }


        /// <summary>
        /// 所属菜单
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Menu Menu { get; set; }

       
    }
}
