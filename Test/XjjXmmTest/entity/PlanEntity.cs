using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmmTest.entity
{
    [Table("MED_FUM_PLANS")]
    public class PlanEntity
    {

        /// <summary>
        /// 唯一号
        /// </summary>
        [Column(ColumnName = "PLAN_ID", IsPrimaryKey = true)]
        public string PlanId { get; set; } = "56637389-ac62-461c-9080-9ee7f36b8e36";

        /// <summary>
        /// 计划时间
        /// </summary>
        [Column(ColumnName = "PLAN_TIME")]
        public DateTime? PlanTime { get; set; } = DateTime.Now;



        /// <summary>
        /// 事件类型
        /// </summary>
        [Column(ColumnName = "EVENT_TYPE", IgnoreSave = true)]
        public string EventType { get; set; } = "EventType1111";

        /// <summary>
        /// 事件名
        /// </summary>
        [Column(ColumnName = "EVENT_NAME", IgnoreSave = true)]
        public string EventName { get; set; }

        /// <summary>
        /// 事件描述
        /// </summary>
        [Column(ColumnName = "EVENT_DESC", IgnoreSave = true)]
        public string EventDesc { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        [Column(ColumnName = "HANDLE_STATUS")]
        public string HandleStatus { get; set; } = "E";

        /// <summary>
        /// 取消原因
        /// </summary>
        [Column(ColumnName = "CANCEL_REASON")]
        public string CancelReason { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        [Column(ColumnName = "HANDLE_NAME")]
        public string HandleName { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [Column(ColumnName = "HANDLE_TIME")]
        public DateTime? HandleTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column(ColumnName = "CREATE_NAME", IgnoreSave = true)]

        public string CreateName { get; set; } = "CREATE_TIME";

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(ColumnName = "CREATE_TIME", IgnoreSave = true)]
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新人
        /// </summary>
        [Column(ColumnName = "UPDATE_NAME")]
        public string UpdateName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column(ColumnName = "UPDATE_TIME")]
        public DateTime? UpdateTime { get; set; }

        [Column(Ignore = true)]
        public string RegistId { get; set; }


        /// <summary>
        /// 随访类型
        /// </summary>
        [Column(Ignore = true)]
        public string FuType { get; set; }

        /// <summary>
        /// 是否失访
        /// </summary>
        [Column(Ignore = true)]
        public string IsLost { get; set; }


        /// <summary>
        /// 是否死亡
        /// </summary>
        [Column(Ignore = true)]
        public string IsDeath { get; set; }

    }
}
