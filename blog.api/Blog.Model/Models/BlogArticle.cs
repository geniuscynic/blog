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
        [SugarColumn(Length = 20)]
        public string Title { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 作者
        /// </summary>
        [SugarColumn(Length = 10)]
        public string Author { get; set; }

        /// <summary>
        /// 引用
        /// </summary>
        public string Quote { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(Length = 4000)]
        public string Content { get; set; }

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
