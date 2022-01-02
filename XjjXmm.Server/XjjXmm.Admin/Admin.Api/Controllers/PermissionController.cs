using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Service.Permission.Input;
using Admin.Service.Permission.Output;
using Admin.Service.Permission;

namespace Admin.Core.Controllers.Admin
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// 查询权限列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PermissionListOutput>> GetList(string key, DateTime? start, DateTime? end)
        {
            return await _permissionService.GetList(key, start, end);
        }

        /// <summary>
        /// 查询单条分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PermissionGetGroupOutput> GetGroup(long id)
        {
            return await _permissionService.GetGroup(id);
        }

        /// <summary>
        /// 查询单条菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PermissionGetMenuOutput> GetMenu(long id)
        {
            return await _permissionService.GetMenu(id);
        }

        /// <summary>
        /// 查询单条接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PermissionGetApiOutput> GetApi(long id)
        {
            return await _permissionService.GetApi(id);
        }

        /// <summary>
        /// 查询单条权限点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PermissionGetDotOutput> GetDot(long id)
        {
            return await _permissionService.GetDot(id);
        }

        /// <summary>
        /// 查询角色权限-权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetPermissionList()
        {
            return await _permissionService.GetPermissionList();
        }

        /// <summary>
        /// 查询角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<long>> GetRolePermissionList(long roleId = 0)
        {
            return await _permissionService.GetRolePermissionList(roleId);
        }

        /// <summary>
        /// 查询租户权限
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<long>> GetTenantPermissionList(long tenantId = 0)
        {
            return await _permissionService.GetTenantPermissionList(tenantId);
        }

        /// <summary>
        /// 新增分组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddGroup(PermissionAddGroupInput input)
        {
            return await _permissionService.AddGroup(input);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddMenu(PermissionAddMenuInput input)
        {
            return await _permissionService.AddMenu(input);
        }

        /// <summary>
        /// 新增接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddApi(PermissionAddApiInput input)
        {
            return await _permissionService.AddApi(input);
        }

        /// <summary>
        /// 新增权限点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddDot(PermissionAddDotInput input)
        {
            return await _permissionService.AddDot(input);
        }

        /// <summary>
        /// 修改分组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateGroup(PermissionUpdateGroupInput input)
        {
            return await _permissionService.UpdateGroup(input);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateMenu(PermissionUpdateMenuInput input)
        {
            return await _permissionService.UpdateMenu(input);
        }

        /// <summary>
        /// 修改接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateApi(PermissionUpdateApiInput input)
        {
            return await _permissionService.UpdateApi(input);
        }

        /// <summary>
        /// 修改权限点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateDot(PermissionUpdateDotInput input)
        {
            return await _permissionService.UpdateDot(input);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _permissionService.SoftDelete(id);
        }

        /// <summary>
        /// 彻底删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Delete(long id)
        {
            return await _permissionService.Delete(id);
        }

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Assign(PermissionAssignInput input)
        {
            return await _permissionService.Assign(input);
        }

        /// <summary>
        /// 保存租户权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> SaveTenantPermissions(PermissionSaveTenantPermissionsInput input)
        {
            return await _permissionService.SaveTenantPermissions(input);
        }
    }
}