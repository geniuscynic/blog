

using SqlSugar;

namespace Admin.Repository.PermissionApi
{
    /// <summary>
    /// 权限接口
    /// </summary>
	[SugarTable("ad_permission_api")]
   
    public class PermissionApiEntity : EntityAdd
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
		public long PermissionId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        //public PermissionEntity Permission { get; set; }

        /// <summary>
        /// 接口Id
        /// </summary>
		public long ApiId { get; set; }

        /// <summary>
        /// 接口
        /// </summary>
        //public ApiEntity Api { get; set; }
    }
}