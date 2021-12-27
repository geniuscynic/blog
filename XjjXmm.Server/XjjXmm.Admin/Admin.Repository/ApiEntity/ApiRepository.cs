using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.ApiEntity
{
    [Injection]
    public class ApiRepository : RepositoryBase<ApiEntity>, IApiRepository
    {
        public ApiRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}