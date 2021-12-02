using Admin.Core.Model.Personnel;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Personnel
{
    [Injection]
    public class OrganizationRepository : RepositoryBase<OrganizationEntity>, IOrganizationRepository
    {
        public OrganizationRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}