using System;
using XjjXmm.DataBase.Utility;

namespace XjjXmmTest.entity {
    
    
    [Table("MED_USERS")]
    public class UserEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Column(ColumnName = "USER_ID", IsPrimaryKey = true)]
        public string Id { get; set; }

      

        /// <summary>
        /// 登录名
        /// </summary>
        [Column(ColumnName = "LOGIN_NAME")]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Column(ColumnName = "LOGIN_PWD")]
        public string LoginPassword { get; set; }


        /// <summary>
        /// 名字
        /// </summary>
        [Column(ColumnName = "USER_NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Column(ColumnName = "DEPT_ID")]
        public string DeptId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Column(Ignore = true)]
        public string DeptName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(ColumnName = "CREATE_DATE", IgnoreSave = true)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 是否有效
        /// 状态值定义：
        /// T:有效
        /// F:无效
        /// </summary>
        [Column(ColumnName = "IS_VALID", IgnoreSave = true)]
        public string IsValid { get; set; } = "T";

        /// <summary>
        /// 备注
        /// </summary>
        [Column(ColumnName = "MEMO")]
        public string Memo { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [Column(ColumnName = "PHONE")]
        public string Phone { get; set; }

        /// <summary>
        /// 医院ID
        /// </summary>
        [Column(ColumnName = "HOSPITAL_ID")]
        public string HospitalId { get; set; }

        ///// <summary>
        ///// 医院ID
        ///// </summary>
        [Column(Ignore = true)]
        public string HospitalName { get; set; }
    }
}
