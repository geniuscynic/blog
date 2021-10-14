using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    public class BaseEntity
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [Column(ColumnName = "create_by", IgnoreSave = true)]
        public string CreateBy { get; set; }

        /// <summary>
        ///  更新人
        /// </summary>
        [Column(ColumnName = "update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        [Column(ColumnName = "create_time", IgnoreSave = true)]
        private DateTime CreateTime { get; set; }

        /// <summary>
        ///  更新时间
        /// </summary>
        [Column(ColumnName = "update_time")]
        private DateTime UpdateTime { get; set; }
    }
}
