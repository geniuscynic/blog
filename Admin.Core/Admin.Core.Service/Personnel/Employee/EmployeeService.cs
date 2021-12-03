using Admin.Core.Common.Attributes;
using Admin.Core.Common.Configs;
using Admin.Core.Common.Helpers;
using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Personnel;
using Admin.Core.Repository;
using Admin.Core.Repository.Personnel;
using Admin.Core.Service.Personnel.Employee.Input;
using Admin.Core.Service.Personnel.Employee.Output;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Service.Personnel.Employee
{
    [Injection]
    /// <summary>
    /// 员工服务
    /// </summary>
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRepositoryBase<EmployeeOrganizationEntity> _employeeOrganizationRepository;

        public EmployeeService(
            IEmployeeRepository userRepository,
            IRepositoryBase<EmployeeOrganizationEntity> employeeOrganizationRepository
        )
        {
            _employeeRepository = userRepository;
            _employeeOrganizationRepository = employeeOrganizationRepository;
        }

        public async Task<EmployeeGetOutput> GetAsync(long id)
        {
            var res = new ResponseOutput<EmployeeGetOutput>();

            var dto = await _employeeRepository.Select
            .WhereDynamic(id)
            .IncludeMany(a => a.Organizations.Select(b => new OrganizationEntity { Id = b.Id }))
            .ToOneAsync(a => new EmployeeGetOutput
            {
                OrganizationName = a.Organization.Name,
                PositionName = a.Position.Name
            });

            return dto;
        }

        public async Task<PageOutput<EmployeeListOutput>> PageAsync(PageInput<EmployeeEntity> input)
        {
            var list = await _employeeRepository.Select
            .WhereDynamicFilter(input.DynamicFilter)
            .Count(out var total)
            .OrderByDescending(true, a => a.Id)
            .IncludeMany(a => a.Organizations.Select(b => new OrganizationEntity { Name = b.Name }))
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync(a => new EmployeeListOutput 
            { 
                OrganizationName = a.Organization.Name, 
                PositionName = a.Position.Name
            });

            var data = new PageOutput<EmployeeListOutput>()
            {
                List = list,
                Total = total
            };

            return data;
        }

        [Transaction]
        public async Task<bool> AddAsync(EmployeeAddInput input)
        {
            var entity = Mapper.Map<EmployeeEntity>(input);
            var employeeId = (await _employeeRepository.InsertAsync(entity))?.Id;

            if (!(employeeId > 0))
            {
                return false;
            }

            //附属部门
            if (input.OrganizationIds != null && input.OrganizationIds.Any())
            {
                var organizations = input.OrganizationIds.Select(organizationId => new EmployeeOrganizationEntity { EmployeeId = employeeId.Value, OrganizationId = organizationId });
                await _employeeOrganizationRepository.InsertAsync(organizations);
            }

            return true;
        }

        [Transaction]
        public async Task<bool> UpdateAsync(EmployeeUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            var employee = await _employeeRepository.GetAsync(input.Id);
            if (!(employee?.Id > 0))
            {
                //return ResponseOutput.NotOk("用户不存在！");
                throw new BussinessException(StatusCodes.Status999Falid, "用户不存在！");
            }

            Mapper.Map(input, employee);
            await _employeeRepository.UpdateAsync(employee);

            await _employeeOrganizationRepository.DeleteAsync(a => a.EmployeeId == employee.Id);

            //附属部门
            if (input.OrganizationIds != null && input.OrganizationIds.Any())
            {
                var organizations = input.OrganizationIds.Select(organizationId => new EmployeeOrganizationEntity { EmployeeId = employee.Id, OrganizationId = organizationId });
                await _employeeOrganizationRepository.InsertAsync(organizations);
            }

            return true;
        }

        [Transaction]
        public async Task<bool> DeleteAsync(long id)
        {
            //删除员工附属部门
            await _employeeOrganizationRepository.DeleteAsync(a => a.EmployeeId == id);

            //删除员工
            await _employeeRepository.DeleteAsync(m => m.Id == id);

            return true;
        }

        public async Task<bool> SoftDeleteAsync(long id)
        {
            await _employeeRepository.SoftDeleteAsync(id);

            return true;
        }

        public async Task<bool> BatchSoftDeleteAsync(long[] ids)
        {
            await _employeeRepository.SoftDeleteAsync(ids);

            return true;
        }
    }
}