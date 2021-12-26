using Admin.Repository.Employee;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Service.Employee
{
    /// <summary>
    /// Ա������
    /// </summary>
    [ProcessLog]
    public interface IEmployeeService
    {
        Task<EmployeeGetOutput> Get(long id);

        Task<PageOutput<EmployeeListOutput>> Page(PageInput<EmployeeEntity> input);

        Task<bool> Add(EmployeeAddInput input);

        Task<bool> Update(EmployeeUpdateInput input);

        Task<bool> Delete(long id);

        Task<bool> SoftDelete(long id);

        Task<bool> BatchSoftDelete(long[] ids);
    }
}