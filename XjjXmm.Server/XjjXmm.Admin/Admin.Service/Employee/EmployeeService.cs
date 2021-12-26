using Admin.Repository.Employee;
using Admin.Repository.EmployeeOrganization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.Employee
{
    /// <summary>
    /// 员工服务
    /// </summary>
    [Injection]
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeOrganizationRepository _employeeOrganizationRepository;

        public EmployeeService(
            IEmployeeRepository userRepository,
            IEmployeeOrganizationRepository employeeOrganizationRepository
        )
        {
            _employeeRepository = userRepository;
            _employeeOrganizationRepository = employeeOrganizationRepository;
        }

        public async Task<EmployeeGetOutput> Get(long id)
        {
            var res = await _employeeRepository.GetById(id);
            var dto = res.MapTo<EmployeeEntity, EmployeeGetOutput>();
            dto.OrganizationName = res.Organization.Name;
            dto.PositionName = res.Position.Name;

            return dto;
        }

        public async Task<PageOutput<EmployeeListOutput>> Page(PageInput<EmployeeEntity> input)
        {
            //var list = await _employeeRepository.Select
            //.WhereDynamicFilter(input.DynamicFilter)
            //.Count(out var total)
            //.OrderByDescending(true, a => a.Id)
            //.IncludeMany(a => a.Organizations.Select(b => new OrganizationEntity { Name = b.Name }))
            //.Page(input.CurrentPage, input.PageSize)
            //.ToListAsync(a => new EmployeeListOutput
            //{
            //    OrganizationName = a.Organization.Name,
            //    PositionName = a.Position.Name
            //});

            var res = await _employeeRepository.QueryPage(input);
            var dto = res.Data.MapTo<EmployeeEntity, EmployeeListOutput>().ToList();
           
            return new PageOutput<EmployeeListOutput>
            {
                CurrentPage = res.CurrentPage,
                Total = res.Total,
                PageSize = res.PageSize,
                Data = dto
            };
        }

       // [Transaction]
        public async Task<bool> Add(EmployeeAddInput input)
        {
            var entity = input.MapTo<EmployeeAddInput, EmployeeEntity>();

            var employeeId = await _employeeRepository.Add(entity);

            if (!(employeeId > 0))
            {
                return false;
            }

            //附属部门
            if (input.OrganizationIds != null && input.OrganizationIds.Any())
            {
                var organizations = input.OrganizationIds.Select(organizationId => new EmployeeOrganizationEntity { EmployeeId = employeeId, OrganizationId = organizationId }).ToList();
                await _employeeOrganizationRepository.Add(organizations);
            }

            return true;
        }

        //[Transaction]
        public async Task<bool> Update(EmployeeUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            //var employee = await _employeeRepository.GetById(input.Id);
            //if (!(employee?.Id > 0))
            //{
            //    //return ResponseOutput.NotOk("用户不存在！");
            //    throw new BussinessException(StatusCodes.Status999Falid, "用户不存在！");
            //}

            var employee = input.MapTo<EmployeeAddInput, EmployeeEntity>();

            var res = await _employeeRepository.Update(employee);
            if(!res)
            {
                throw new BussinessException(StatusCodes.Status999Falid, "用户不存在！");
            }

            await _employeeOrganizationRepository.Delete(a => a.EmployeeId == employee.Id);

            //附属部门
            if (input.OrganizationIds != null && input.OrganizationIds.Any())
            {
                var organizations = input.OrganizationIds.Select(organizationId => new EmployeeOrganizationEntity { EmployeeId = employee.Id, OrganizationId = organizationId }).ToList();
                await _employeeOrganizationRepository.Add(organizations);
            }

            return true;
        }

       // [Transaction]
        public async Task<bool> Delete(long id)
        {
            //删除员工附属部门
            await _employeeOrganizationRepository.Delete(a => a.EmployeeId == id);

            //删除员工
            await _employeeRepository.Delete(m => m.Id == id);

            return true;
        }

        public async Task<bool> SoftDelete(long id)
        {
            await _employeeRepository.SoftDelete(id);

            return true;
        }

        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            await _employeeRepository.SoftDelete(ids);

            return true;
        }
    }
}