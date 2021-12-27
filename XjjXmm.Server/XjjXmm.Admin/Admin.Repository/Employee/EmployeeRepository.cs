
using SqlSugar;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.Employee
{
    [Injection]
    public class EmployeeRepository : RepositoryBase<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(ISqlSugarClient context) : base(context)
        {
           
        }


        public async Task<EmployeeEntity> GetById(long id)
        {
            var dto = await _context.Queryable<EmployeeEntity>().In(id)
                .Mapper(t=>t.Organization, t=>t.OrganizationId, t=>t.Organization.Id)
                .Mapper(t => t.Position, t => t.PositionId, t => t.Position.Id)
                .FirstAsync();
                
           

            return dto;
        }


        public async Task<PageOutput<EmployeeEntity>> QueryPage(PageInput<EmployeeEntity> input)
        {
            var query = _context.Queryable<EmployeeEntity>();

            if (!string.IsNullOrEmpty(input.Filter?.Name))
            {
                query = query.Where(t => t.Name == input.Filter.Name);
            }

            RefAsync<int> total = 0;
            var res = await query.OrderBy(t => t.Id, OrderByType.Desc)
                .Mapper(t => t.Organization, t => t.OrganizationId, t => t.Organization.Id)
                .Mapper(t => t.Position, t => t.PositionId, t => t.Position.Id)
                .ToPageListAsync(input.CurrentPage, input.PageSize, total);

            return new PageOutput<EmployeeEntity>
            {
                CurrentPage = input.CurrentPage,
                Total = total.Value,
                PageSize = input.PageSize,
                Data = res
            };
        }
    }
}