using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Entity.RootTkey;

namespace Blog.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : RootEntityTkey<int>
    {
        /// <summary>
        /// code
        /// </summary>
        [SugarColumn(Length = 10, ColumnDataType = "nvarchar")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(Length = 10, ColumnDataType = "nvarchar")]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(Length = 50, ColumnDataType = "nvarchar")]
        public string Description { get; set; } = "";



    }
}
