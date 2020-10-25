using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Models
{
    /// <summary>
    /// blog 类
    /// </summary>
    public class BlogArticle : RootEntityTkey<int>
    {
        

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 引用
        /// </summary>
        public string Quote { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Category Category { get; set; }
    }
}
