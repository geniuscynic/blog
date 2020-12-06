using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.ViewModels
{
    /// <summary>
    /// blog 类
    /// </summary>
    public class ListBlogViewModel
    {
        /// <summary>
        /// blog id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 引用
        /// </summary>
        public string Quote { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishDate { get; set; }

    }
}
