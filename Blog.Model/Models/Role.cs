using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : RootEntityTkey<int>
    {
        /// <summary>
        /// code
        /// </summary>
        [SugarColumn(Length = 10)]
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
