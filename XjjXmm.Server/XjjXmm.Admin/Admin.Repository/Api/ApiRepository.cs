using Admin.Repository.Api.Entity;
using SqlSugar;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.Api
{
    [Injection]
    public class ApiRepository : RepositoryBase<ApiEntity>, IApiRepository
    {
        public ApiRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<PageOutput<ApiEntity>> Page(PageInput<ApiEntity> input)
        {
            var key = input.Filter?.Label;

            RefAsync<int> total = 0;
            var list = await _context.Queryable<ApiEntity>()
                .WhereIF(!string.IsNullOrEmpty(key), a => a.Path.Contains(key) || a.Label.Contains(key))
                .ToPageListAsync(input.CurrentPage, input.PageSize, total);

           
            return new PageOutput<ApiEntity>
            {
                CurrentPage = input.CurrentPage,
                Total = total.Value,
                PageSize = input.PageSize,
                Data = list
            };
            //var list = await _apiRepository.Select
            //.WhereIf(key.NotNull(), a => a.Path.Contains(key) || a.Label.Contains(key))
            //.Count(out var total)
            //.OrderByDescending(true, c => c.Id)
            //.Page(input.CurrentPage, input.PageSize)
            //.ToListAsync();
        }
    }
}