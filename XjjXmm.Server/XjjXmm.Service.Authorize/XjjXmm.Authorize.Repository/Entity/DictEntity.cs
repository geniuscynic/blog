using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    [Table("sys_dict")]
    public class DictEntity : BaseEntity
    {


        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimaryKey = true, ColumnName = "dict_id", IsIdentity = true)]
        public long Id { get; set; }

       

        /// <summary>
        ///  名称
        /// </summary>
        [Column(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        ///  描述
        /// </summary>
        [Column(ColumnName = "description")]
        public string Description { get; set; }

       
    }
}
