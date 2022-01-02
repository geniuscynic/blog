
using Admin.Repository.Role;
using Admin.Service.Role;
using Admin.Service.Role.Input;
using Admin.Service.Role.Output;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;

namespace Admin.Core.Controllers.Admin
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 查询单条角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RoleGetOutput> Get(long id)
        {
            return await _roleService.Get(id);
        }

        /// <summary>
        /// 查询分页角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<RoleListOutput>> GetPage(PageInput<RoleListInput> model)
        {
            return await _roleService.Page(model);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(RoleAddInput input)
        {
            return await _roleService.Add(input);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(RoleUpdateInput input)
        {
            return await _roleService.Update(input);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _roleService.SoftDelete(id);
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _roleService.BatchSoftDelete(ids);
        }
    }
}