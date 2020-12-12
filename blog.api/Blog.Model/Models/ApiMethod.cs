using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Models
{
    /// <summary>
    /// 按钮配置
    /// </summary>
    public class ApiMethod : RootEntityTkey<int>
    {

        /// <summary>
        /// 按钮 code
        /// </summary>
        [SugarColumn(Length = 100,ColumnDataType = "nvarchar")]
        public string RoutePath { get; set; } = "";


        /// <summary>
        /// 按钮名
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string HttpMethod { get; set; }

        /// <summary>
        /// action
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Action { get; set; }

        /// <summary>
        /// controller
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Controller { get; set; }

    }
}
