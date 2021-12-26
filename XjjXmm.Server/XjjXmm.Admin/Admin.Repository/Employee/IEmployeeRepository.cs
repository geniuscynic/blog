
using XjjXmm.FrameWork.Common;

namespace Admin.Repository.Employee
{
    public interface IEmployeeRepository : IRepositoryBase<EmployeeEntity>
    {
        Task<PageOutput<EmployeeEntity>> QueryPage(PageInput<EmployeeEntity> input);
    }
}