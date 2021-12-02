using FreeSql;
using System;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository
{
    [Injection]
    public class MyUnitOfWorkManager : UnitOfWorkManager
    {
        public MyUnitOfWorkManager(IdleBus<IFreeSql> ib, IServiceProvider serviceProvider) : base(ib.GetFreeSql(serviceProvider))
        {
        }
    }
}