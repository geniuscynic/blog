using Admin.Repository.Permission;
using Admin.Repository.RolePermission;
using Admin.Repository.UserRole;
using SqlSugar;
using System.Linq.Expressions;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.View
{
    [Injection]
    public class ViewRepository : RepositoryBase<ViewEntity>, IViewRepository
    {
        public ViewRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<List<ViewEntity>> List(string key)
        {
            var query = _context.Queryable<ViewEntity>();

           // var key = input.Filter?.Label;
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(a => a.Path.Contains(key) || a.Label.Contains(key));
            }



           
            var res = await query
                .OrderBy(a => a.ParentId)
                .OrderBy(a => a.Sort)
                .ToListAsync();

            return res;

            //throw new NotImplementedException();
        }

        public async Task<PageOutput<ViewEntity>> QueryPage(PageInput<ViewEntity> input)
        {
            var query = _context.Queryable<ViewEntity>();

            var key = input.Filter?.Label;
            if (!string.IsNullOrEmpty(input.Filter?.Label))
            {
                query = query.Where(a => a.Path.Contains(key) || a.Label.Contains(key));
            }



            RefAsync<int> total = 0;
            var res = await query
                .OrderBy(t => t.Id, OrderByType.Desc)
                .ToPageListAsync(input.CurrentPage, input.PageSize, total);

            return new PageOutput<ViewEntity>
            {
                CurrentPage = input.CurrentPage,
                Total = total,
                PageSize = input.PageSize,
                Data = res
            };
        }
    }
}