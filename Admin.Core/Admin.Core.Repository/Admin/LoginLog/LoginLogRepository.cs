using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class LoginLogRepository : RepositoryBase<LoginLogEntity>, ILoginLogRepository
    {
        public LoginLogRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}