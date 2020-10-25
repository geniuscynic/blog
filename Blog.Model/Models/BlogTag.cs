using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;


namespace Blog.Core.Models
{
    /// <summary>
    /// 标签
    /// </summary>
    public class BlogTag : RootEntityTkey<int>
    {
       

        /// <summary>
        /// blog id
        /// </summary>
        public int BlogId { get; set; }

        /// <summary>
        /// tag id
        /// </summary>
        public int TagId { get; set; }
    }
}
