using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    [Table("sys_users_jobs")]
    public class UserJobEntity
    {
        /// <summary>
        ///  岗位名称
        /// </summary>
        [Column(ColumnName = "user_id")]
        public string UserId { get; set; }

        /// <summary>
        ///  岗位排序
        /// </summary>
        [Column(ColumnName = "job_id")]
        public long JobId { get; set; }


    }
}
