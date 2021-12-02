using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class ApiRepository : RepositoryBase<ApiEntity>, IApiRepository
    {
        
        public ApiRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}