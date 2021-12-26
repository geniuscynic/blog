using Admin.Repository;
using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.Organization
{
    [Injection]
    public class OrganizationRepository : RepositoryBase<OrganizationEntity>, IOrganizationRepository
    {
        public OrganizationRepository(ISqlSugarClient context) : base(context)
        {

        }

        public async Task<List<OrganizationEntity>> GetList(string key)
        {

            var queryable = _context.Queryable<OrganizationEntity>()
                .OrderBy(a => a.ParentId)
               .OrderBy(a => a.Sort);

            if (!string.IsNullOrEmpty(key))
            {
                queryable = queryable.Where(a => a.Name.Contains(key) || a.Code.Contains(key));
            }


            return await queryable.ToListAsync();


        }
    }
}