
using System;
using System.Collections.Generic;
using System.Text;
using DoCare.Zkzx.Core.Database.Utility;


namespace Permission.Entity
{
    /// <summary>
    /// 用户类
    /// </summary>
    [Table("BlogUser")]
    public class UserEntity
    {
        [Column(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        //[SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
       // [SugarColumn(Length = 20, ColumnDataType = "nvarchar")]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
       // [SugarColumn(Length = 6, ColumnDataType = "nvarchar")]
        public string NickName { get; set; }

        /// <summary>
        /// 最后登入时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(IgnoreSave = true)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column(IgnoreSave = true)]
        public string CreateBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdatedBy { get; set; }


        /// <summary>
        ///  0 删除， 1 正常状态
        /// </summary>
        [Column(IgnoreSave = true)]
        public int Status { get; set; } = 1;

        
    }
}
