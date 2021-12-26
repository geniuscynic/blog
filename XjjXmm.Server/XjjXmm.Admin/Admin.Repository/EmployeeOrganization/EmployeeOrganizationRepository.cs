
using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.EmployeeOrganization
{
    [Injection]
    public class EmployeeOrganizationRepository : RepositoryBase<EmployeeOrganizationEntity>, IEmployeeOrganizationRepository
    {
        public EmployeeOrganizationRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}