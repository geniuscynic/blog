using System;
using DoCare.Zkzx.Core.Database.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    /// <summary>
    /// 用户类
    /// </summary>
    [Table("xjjxmm_user")]
    public class UserEntity
    {
        [Column(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Column(IgnoreSave = true)]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }


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
