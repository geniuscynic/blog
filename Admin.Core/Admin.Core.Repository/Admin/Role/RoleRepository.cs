using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
    {
        public RoleRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}