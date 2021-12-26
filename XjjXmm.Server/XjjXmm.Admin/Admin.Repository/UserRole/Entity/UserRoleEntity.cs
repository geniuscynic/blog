using SqlSugar;

namespace Admin.Repository.UserRole
{
    /// <summary>
    /// 用户角色
    /// </summary>
	[SugarTable("ad_user_role")]

    public class UserRoleEntity   :EntityAdd
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        //public UserEntity User { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        //public RoleEntity Role { get; set; }


      
    }
}