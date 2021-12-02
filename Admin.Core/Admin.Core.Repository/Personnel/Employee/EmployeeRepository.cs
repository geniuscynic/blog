using Admin.Core.Model.Personnel;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Personnel
{
    [Injection]
    public class EmployeeRepository : RepositoryBase<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}