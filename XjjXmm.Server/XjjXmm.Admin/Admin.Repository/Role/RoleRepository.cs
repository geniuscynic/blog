
using SqlSugar;
using System.Linq.Expressions;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.Role
{
    [Injection]
    public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
    {
        public RoleRepository(ISqlSugarClient context) : base(context)
        {
        }

       
    }
}