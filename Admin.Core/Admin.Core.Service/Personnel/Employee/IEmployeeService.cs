using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Personnel;
using Admin.Core.Service.Personnel.Employee.Input;
using Admin.Core.Service.Personnel.Employee.Output;
using System.Threading.Tasks;

namespace Admin.Core.Service.Personnel.Employee
{
    /// <summary>
    /// 员工服务
    /// </summary>
    public interface IEmployeeService
    {
        Task<EmployeeGetOutput> GetAsync(long id);

        Task<PageOutput<EmployeeListOutput>> PageAsync(PageInput<EmployeeEntity> input);

        Task<bool> AddAsync(EmployeeAddInput input);

        Task<bool> UpdateAsync(EmployeeUpdateInput input);

        Task<bool> DeleteAsync(long id);

        Task<bool> SoftDeleteAsync(long id);

        Task<bool> BatchSoftDeleteAsync(long[] ids);
    }
}