using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmmTest.entity
{
    [Table("MED_FUM_FORM_DEFAULT")]
    public class FormDefaultEntity
    {

        /// <summary>
        /// 唯一号
        /// </summary>
        [Column(ColumnName = "ID", IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 随访单号
        /// </summary>
        [Column(ColumnName = "FORM_ID")]
        public string FormId { get; set; }

        /// <summary>
        /// 随访单内容
        /// </summary>
        [Column(ColumnName = "CONTENT", IsBigText = true)]
        public string Content { get; set; }

    }


}

