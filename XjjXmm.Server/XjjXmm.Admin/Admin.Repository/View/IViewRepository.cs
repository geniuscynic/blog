using XjjXmm.FrameWork.Common;

namespace Admin.Repository.View;

public interface IViewRepository : IRepositoryBase<ViewEntity>
{
    Task<List<ViewEntity>> List(string key);

    //Task<IEnumerable<ViewEntity>> GetByUserId(long userId); 
    Task<PageOutput<ViewEntity>> QueryPage(PageInput<ViewEntity> input);
}
