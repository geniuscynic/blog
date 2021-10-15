using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    [Table("sys_job")]
    public class JobEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimaryKey = true, ColumnName = "job_id", IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        ///  岗位名称
        /// </summary>
        [Column(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        ///  岗位排序
        /// </summary>
        [Column(ColumnName = "jobSort")]
        public long JobSort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column(ColumnName = "enabled")]
        private bool Enabled { get; set; }
    }
}
