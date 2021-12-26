using Admin.Repository.Permission;
using SqlSugar;
using System.Linq.Expressions;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.RolePermission
{
    [Injection]
    public class RolePermissionRepository : RepositoryBase<RolePermissionEntity>, IRolePermissionRepository
    {
        public RolePermissionRepository(ISqlSugarClient context) : base(context)
        {
        }

       
    }
}