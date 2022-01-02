using Admin.Repository.Api.Entity;
using Admin.Repository.Organization;
using XjjXmm.FrameWork.Common;

namespace Admin.Repository.Api
{
    public interface IApiRepository : IRepositoryBase<ApiEntity>
    {
        Task<PageOutput<ApiEntity>> Page(PageInput<ApiEntity> input);
    }
}