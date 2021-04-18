using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Entity.RootTkey;


namespace Blog.Entity
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

        /// <summary>
        /// 对应的blog
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public BlogArticle Blog { get; set; }


        /// <summary>
        /// 对应的 tag
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Tag Tag { get; set; }
    }
}
