using Admin.Repository.RolePermission;
using Admin.Repository.UserRole;
using Admin.Repository.View;
using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.PermissionApi
{
    [Injection]
    public class PermissionApiRepository : RepositoryBase<PermissionApiEntity>, IPermissionApiRepository
    {
        public PermissionApiRepository(ISqlSugarClient context) : base(context)
        {
        }


    }
}