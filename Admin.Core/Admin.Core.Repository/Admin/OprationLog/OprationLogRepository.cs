using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class OprationLogRepository : RepositoryBase<OprationLogEntity>, IOprationLogRepository
    {
        public OprationLogRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}