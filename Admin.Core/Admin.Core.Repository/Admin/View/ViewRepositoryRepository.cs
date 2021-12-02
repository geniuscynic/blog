using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class ViewRepository : RepositoryBase<ViewEntity>, IViewRepository
    {
        public ViewRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}