using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Repository
{
    public class EntityFull
    {
        

        /// <summary>
        /// 版本
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 创建者Id
        /// </summary>
        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public long? CreatedUserId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public string? CreatedUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 修改者Id
        /// </summary>
        public long? ModifiedUserId { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string? ModifiedUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }
    }

    public class EntityAdd
    {



        /// <summary>
        /// 创建者Id
        /// </summary>
        public long? CreatedUserId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreatedUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        
    }
}
