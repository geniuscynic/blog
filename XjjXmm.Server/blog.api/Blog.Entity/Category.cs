using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Entity.RootTkey;


namespace Blog.Entity
{
    /// <summary>
    /// 分类
    /// </summary>
    public class Category : RootEntityTkey<int>
    {


        /// <summary>
        /// 分类名
        /// </summary>
        [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Name { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int SeqNum { get; set; }

        /// <summary>
        /// 层数
        /// </summary>
        public int Floor { get; set; }


        /// <summary>
        /// parentId
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 下级分类
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Category> SubCategories { get; set; }

        /// <summary>
        /// 父分类
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Category ParentCategory { get; set; }

        /// <summary>
        /// 对应blog
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<BlogArticle> Blogs { get; set; }
    }
}
