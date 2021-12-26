using Admin.Repository.Permission;
using Admin.Repository.RolePermission;
using Admin.Repository.UserRole;
using SqlSugar;
using System.Linq.Expressions;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.View
{
    [Injection]
    public class ViewRepository : RepositoryBase<ViewEntity>, IViewRepository
    {
        public ViewRepository(ISqlSugarClient context) : base(context)
        {
        }

       
    }
}