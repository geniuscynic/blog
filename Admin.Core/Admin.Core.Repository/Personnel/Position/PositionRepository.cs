using Admin.Core.Model.Personnel;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Personnel
{
    [Injection]
    public class PositionRepository : RepositoryBase<PositionEntity>, IPositionRepository
    {
        public PositionRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}