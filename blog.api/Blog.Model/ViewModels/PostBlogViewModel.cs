using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.ViewModels
{
    /// <summary>
    /// blog 类
    /// </summary>
    public class PostBlogViewModel
    {
        /// <summary>
        /// 标题
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
        public int CategoryId { get; set; }

        /// <summary>
        ///  分类
        /// </summary>
        public HashSet<string> Tags { get; set; } = new HashSet<string>();


    }
}
