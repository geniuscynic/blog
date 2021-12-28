using Admin.Repository.Organization;
using XjjXmm.FrameWork.Common;

namespace Admin.Repository.ApiEntity
{
    public interface IApiRepository : IRepositoryBase<ApiEntity>
    {
        Task<PageOutput<ApiEntity>> Page(PageInput<ApiEntity> input);
    }
}