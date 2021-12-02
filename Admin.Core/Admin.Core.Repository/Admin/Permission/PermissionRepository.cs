using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class PermissionRepository : RepositoryBase<PermissionEntity>, IPermissionRepository
    {
        public PermissionRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}