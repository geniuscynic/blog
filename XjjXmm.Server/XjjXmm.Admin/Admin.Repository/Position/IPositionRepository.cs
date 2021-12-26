
using XjjXmm.FrameWork.Common;

namespace Admin.Repository.Position
{
    public interface IPositionRepository : IRepositoryBase<PositionEntity>
    {
        Task<PageOutput<PositionEntity>> QueryPage(PageInput<PositionEntity> input);
    }
}