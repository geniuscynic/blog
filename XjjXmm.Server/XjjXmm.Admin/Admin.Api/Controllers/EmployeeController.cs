
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Admin.Service.Employee;
using XjjXmm.FrameWork.Common;
using Admin.Repository.Employee;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// 员工管理
    /// </summary>
    [ApiController]
    [Route("api/personnel/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// 查询单条员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<EmployeeGetOutput> Get(long id)
        {
            return await _employeeService.Get(id);
        }

        /// <summary>
        /// 查询分页员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        //[ResponseCache(Duration = 60)]
        public async Task<PageOutput<EmployeeListOutput>> GetPage(PageInput<EmployeeEntity> input)
        {
            return await _employeeService.Page(input);
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(EmployeeAddInput input)
        {
            return await _employeeService.Add(input);
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(EmployeeUpdateInput input)
        {
            return await _employeeService.Update(input);
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _employeeService.SoftDelete(id);
        }

        /// <summary>
        /// 批量删除员工
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _employeeService.BatchSoftDelete(ids);
        }
    }
}