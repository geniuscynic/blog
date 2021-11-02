using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    [Table("sys_dept")]
    public class DeptEntity : BaseEntity
    {


        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimaryKey = true, ColumnName = "dept_id", IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        ///  排序
        /// </summary>
        [Column(ColumnName = "dept_sort")]
        public int DeptSort { get; set; }

        /// <summary>
        ///  部门名称
        /// </summary>
        [Column(ColumnName = "name")]
        public string Name { get; set; }


        /// <summary>
        ///  是否启用
        /// </summary>
        [Column(ColumnName = "enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        ///  上级部门
        /// </summary>
        [Column(ColumnName = "pid")]
        public long Pid { get; set; }

        /// <summary>
        ///  子节点数目
        /// </summary>
        [Column(ColumnName = "sub_count")]
        public long SubCount { get; set; }


      


    }
}
