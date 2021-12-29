
using SqlSugar;
using System.Linq.Expressions;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.Role
{
    [Injection]
    public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
    {
        public RoleRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<PageOutput<RoleEntity>> Page(PageInput<RoleEntity> input)
        {
            var key = input.Filter?.Name;

            var query = _context.Queryable<RoleEntity>();

            if (!string.IsNullOrEmpty(input.Filter?.Name))
            {
                query = query.Where(t => t.Name == input.Filter.Name);
            }

            RefAsync<int> total = 0;
            var res = await query.OrderBy(t => t.Id, OrderByType.Desc).ToPageListAsync(input.CurrentPage, input.PageSize, total);

            return new PageOutput<RoleEntity>
            {
                CurrentPage = input.CurrentPage,
                Total = total,
                PageSize = input.PageSize,
                Data = res
            };
        }
    }
}