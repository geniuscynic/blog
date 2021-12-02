using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{     
    [Injection]
    public class TenantRepository : RepositoryBase<TenantEntity>, ITenantRepository
    {
        
        public TenantRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}