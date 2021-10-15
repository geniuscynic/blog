using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    [Table("sys_dict_detail")]
    public class DictDetailEntity : BaseEntity
    {


        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimaryKey = true, ColumnName = "detail_id", IsIdentity = true)]
        public long Id { get; set; }


        /// <summary>
        ///  字典标签
        /// </summary>
        [Column(ColumnName = "label")]
        public string Label { get; set; }

        /// <summary>
        ///  字典值
        /// </summary>
        [Column(ColumnName = "value")]
        public string Value { get; set; }

        /// <summary>
        ///  排序
        /// </summary>
        [Column(ColumnName = "dictSort")]
        public int DictSort { get; set; } = 999;
    }
}
