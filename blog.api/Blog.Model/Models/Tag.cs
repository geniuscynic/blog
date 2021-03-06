﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;


namespace Blog.Core.Models
{
    /// <summary>
    /// 标签
    /// </summary>
    public class Tag : RootEntityTkey<int>
    {

        /// <summary>
        /// 标签名
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Name { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<BlogArticle> Blogs { get; set; }
    }
}
