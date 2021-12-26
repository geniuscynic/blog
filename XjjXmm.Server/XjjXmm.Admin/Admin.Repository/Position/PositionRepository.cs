using SqlSugar;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.Position
{
    [Injection]
    public class PositionRepository : RepositoryBase<PositionEntity>, IPositionRepository
    {
        public PositionRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<PageOutput<PositionEntity>> QueryPage(PageInput<PositionEntity> input)
        {
            var query = _context.Queryable<PositionEntity>();

            if (!string.IsNullOrEmpty(input.Filter?.Name))
            {
                query = query.Where(t => t.Name == input.Filter.Name);
            }

            RefAsync<int> total = 0;
            var res = await query.OrderBy(t=>t.Id, OrderByType.Desc).ToPageListAsync(input.CurrentPage, input.PageSize, total);

            return new PageOutput<PositionEntity>
            {
                CurrentPage = input.CurrentPage,
                Total = total,
                PageSize = input.PageSize,
                Data = res
            };
        }
    }
}