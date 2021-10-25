using XjjXmm.DataBase.Utility;

namespace XjjXmmTest.blog
{
    /// <summary>
    /// 用户,角色关联类
    /// </summary>
    [Table("UserRole")]
    public class BlogUserRoleEntity
    {
        [Column(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 对应的用户
        /// </summary>
        [Column(Ignore = true)]
         public BlogUserEntity User { get; set; }

        /// <summary>
        /// 对应的角色
        /// </summary>
         [Column(Ignore = true)]
         public BlogRoleEntity Role {get;set;}
    }
}
