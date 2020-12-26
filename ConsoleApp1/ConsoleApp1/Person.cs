using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1.attribute;

namespace ConsoleApp1
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class BlogArticle 
    {
        [XjjxmmKey]
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>

        [XjjxmmField(FieldName = "title11")]
        public string Title { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishDate { get; set; } = DateTime.Now;

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
        public int CategoryId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
      
        public string Content { get; set; }

    }
}
