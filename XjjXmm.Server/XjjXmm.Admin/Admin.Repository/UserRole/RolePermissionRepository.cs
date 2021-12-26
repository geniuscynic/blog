using Admin.Repository.Permission;
using SqlSugar;
using System.Linq.Expressions;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.UserRole
{
    [Injection]
    public class UserRoleRepository : RepositoryBase<UserRoleEntity>, IUserRoleRepository
    {
        public UserRoleRepository(ISqlSugarClient context) : base(context)
        {
        }

       
    }
}