using System;
using System.Collections.Generic;
using XjjXmm.DataBase.Utility;

namespace XjjXmmTest.entity
{
    /// <summary>
    /// 随访单主表
    /// </summary>
    [Table("MED_FUM_FORM")]
    public class FormEntity 
    {

        /// <summary>
		/// 唯一号
		/// </summary>
		[Column(ColumnName ="ID", IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 登记号
        /// </summary>
        [Column(ColumnName ="REGIST_ID")]
        public string RegistId { get; set; }

        /// <summary>
        /// 计划号
        /// </summary>
        [Column(ColumnName ="PLAN_ID")]
        public string PlanId { get; set; }

        /// <summary>
        /// 随访类型
        /// </summary>
        [Column(ColumnName ="FU_TYPE")]
        public string FuType { get; set; }

        /// <summary>
        /// 是否失访
        /// </summary>
        [Column(ColumnName ="IS_LOST")]
        public string IsLost { get; set; }

        /// <summary>
        /// 失访原因
        /// </summary>
        [Column(ColumnName = "LOST_OTHER_REASON")]
        public string LostOtherReason { get; set; }

        /// <summary>
        /// 失访原因
        /// </summary>
        [Column(ColumnName = "LOST_REASON")]
        public string LostReason { get; set; }

        /// <summary>
        /// 是否死亡
        /// </summary>
        [Column(ColumnName ="IS_DEATH")]
        public string IsDeath { get; set; }

        /// <summary>
        /// 死亡时间
        /// </summary>
        [Column(ColumnName ="DEATH_TIME")]
        public DateTime? DeathTime { get; set; }

        /// <summary>
        /// 死亡时间
        /// </summary>
        [Column(ColumnName = "DEATH_REASON")]
        public string DeathReason { get; set; }


        /// <summary>
        /// 死亡时间
        /// </summary>
        [Column(ColumnName = "DAUK_NO")]
        public string DaukNo { get; set; }

        
        /// <summary>
        /// 门诊号
        /// </summary>
        [Column(ColumnName ="MZ_NO")]
        public string MzNo { get; set; }

        /// <summary>
        /// 门诊就诊号
        /// </summary>
        [Column(ColumnName ="MZ_VISIT_ID")]
        public string MzVisitId { get; set; }

        /// <summary>
        /// 门诊就诊时间
        /// </summary>
        [Column(ColumnName ="MZ_VISIT_TIME")]
        public DateTime? MzVisitTime { get; set; }

        /// <summary>
        /// 随访单状态
        /// </summary>
        [Column(ColumnName ="FORM_STATUS")]
        public string FormStatus { get; set; }

        /// <summary>
        /// 随访时间
        /// </summary>
        [Column(ColumnName = "FU_TIME")]
        public DateTime? FollowTime { get; set; }

        /// <summary>
        /// 下次随访时间
        /// </summary>
        [Column(ColumnName = "PLAN_NEXT_TIME")]
        public DateTime? PlanNextTime { get; set; }

        /// <summary>
        /// 随访医生
        /// </summary>
        [Column(ColumnName ="FU_DOCTOR_ID")]
        public string FuDoctorId { get; set; }

        /// <summary>
        /// 随访医生名
        /// </summary>
        [Column(ColumnName ="FU_DOCTOR_NAME")]
        public string FuDoctorName { get; set; }


        [Column(Ignore = true)]
        public IEnumerable<FormDefaultEntity> FormDefaultEntity
        {
            get;
            set;
        }
    }
}
