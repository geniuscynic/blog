using SqlSugar;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.User
{
    [Injection]
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<PageOutput<UserEntity>> QueryPage(PageInput<UserEntity> input)
        {
            var query = _context.Queryable<UserEntity>();

            //if (!string.IsNullOrEmpty(input.Filter?.Name))
            //{
            //    query = query.Where(t => t.Name == input.Filter.Name);
            //}

            RefAsync<int> total = 0;
            var res = await query.OrderBy(t => t.Id, OrderByType.Desc).ToPageListAsync(input.CurrentPage, input.PageSize, total);

            return new PageOutput<UserEntity>
            {
                CurrentPage = input.CurrentPage,
                Total = total,
                PageSize = input.PageSize,
                Data = res
            };
        }
    }
}