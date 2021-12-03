using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.Role;
using Admin.Core.Service.Admin.Role.Input;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.Role.Output;

namespace Admin.Core.Controllers.Admin
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : AreaController
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
            return await _roleService.GetAsync(id);
        }

        /// <summary>
        /// 查询分页角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<RoleListOutput>> GetPage(PageInput<RoleEntity> model)
        {
            return await _roleService.PageAsync(model);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(RoleAddInput input)
        {
            return await _roleService.AddAsync(input);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(RoleUpdateInput input)
        {
            return await _roleService.UpdateAsync(input);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _roleService.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _roleService.BatchSoftDeleteAsync(ids);
        }
    }
}