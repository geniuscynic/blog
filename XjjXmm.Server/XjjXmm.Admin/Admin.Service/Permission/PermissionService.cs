using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using Admin.Service.Permission.Input;
using Admin.Service.Permission.Output;
using Admin.Repository.Permission;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.Permission
{
    [Injection]
    public class PermissionService : BaseService, IPermissionService
    {
        //private readonly AppConfig _appConfig;
        private readonly IPermissionRepository _permissionRepository;
        //private readonly IRoleRepository _roleRepository;
        //private readonly IRepositoryBase<RolePermissionEntity> _rolePermissionRepository;
        //private readonly IRepositoryBase<TenantPermissionEntity> _tenantPermissionRepository;
        //private readonly IRepositoryBase<UserRoleEntity> _userRoleRepository;
        //private readonly IRepositoryBase<PermissionApiEntity> _permissionApiRepository;

        public PermissionService(
           // AppConfig appConfig,
            IPermissionRepository permissionRepository
            //IRoleRepository roleRepository,
            //IRepositoryBase<RolePermissionEntity> rolePermissionRepository,
            //IRepositoryBase<TenantPermissionEntity> tenantPermissionRepository,
            //IRepositoryBase<UserRoleEntity> userRoleRepository,
            //IRepositoryBase<PermissionApiEntity> permissionApiRepository
        )
        {
            //_appConfig = appConfig;
            _permissionRepository = permissionRepository;
            //_roleRepository = roleRepository;
            //_rolePermissionRepository = rolePermissionRepository;
            //_tenantPermissionRepository = tenantPermissionRepository;
            //_userRoleRepository = userRoleRepository;
            //_permissionApiRepository = permissionApiRepository;
        }

        /// <summary>
        /// 清除权限下关联的用户权限缓存
        /// </summary>
        /// <param name="permissionIds"></param>
        /// <returns></returns>
        private async Task ClearUserPermissions(List<long> permissionIds)
        {
            //var userIds = await _userRoleRepository.Select.Where(a =>
            //    _rolePermissionRepository
            //    .Where(b => b.RoleId == a.RoleId && permissionIds.Contains(b.PermissionId))
            //    .Any()
            //).ToList(a => a.UserId);
            //foreach (var userId in userIds)
            //{
            //    await Cache.Del(string.Format(CacheKey.UserPermissions, userId));
            //}

            throw new NotImplementedException();
        }

        public async Task<PermissionEntity> Get(long id)
        {
            //var result = await _permissionRepository.Get(id);

            //return result;
            throw new NotImplementedException();
        }

        public async Task<PermissionGetGroupOutput> GetGroup(long id)
        {
            //var result = await _permissionRepository.GetAsync<PermissionGetGroupOutput>(id);
            //return result;
            throw new NotImplementedException();
        }

        public async Task<PermissionGetMenuOutput> GetMenu(long id)
        {
            //var result = await _permissionRepository.GetAsync<PermissionGetMenuOutput>(id);
            //return result;
            throw new NotImplementedException();
        }

        public async Task<PermissionGetApiOutput> GetApi(long id)
        {
            //var result = await _permissionRepository.GetAsync<PermissionGetApiOutput>(id);
            //return result;
            throw new NotImplementedException();
        }

        public async Task<PermissionGetDotOutput> GetDot(long id)
        {
            //var entity = await _permissionRepository.Select
            //.WhereDynamic(id)
            //.IncludeMany(a => a.Apis.Select(b => new ApiEntity { Id = b.Id }))
            //.ToOne();

            //var output = Mapper.Map<PermissionGetDotOutput>(entity);

            //return output;
            throw new NotImplementedException();
        }

        public async Task<List<PermissionListOutput>> GetList(string key, DateTime? start, DateTime? end)
        {
            //if (end.HasValue)
            //{
            //    end = end.Value.AddDays(1);
            //}

            //var data = await _permissionRepository
            //    .WhereIf(key.NotNull(), a => a.Path.Contains(key) || a.Label.Contains(key))
            //    .WhereIf(start.HasValue && end.HasValue, a => a.CreatedTime.Value.BetweenEnd(start.Value, end.Value))
            //    .OrderBy(a => a.ParentId)
            //    .OrderBy(a => a.Sort)
            //    .ToList(a => new PermissionListOutput { ApiPaths = string.Join(";", _permissionApiRepository.Where(b => b.PermissionId == a.Id).ToList(b => b.Api.Path)) });

            //return data;

            var entity = await _permissionRepository.GetList(key, start, end);

            var dto = entity.MapTo<PermissionEntity, PermissionListOutput>();

            return dto.ToList();
        }

        public async Task<bool> AddGroup(PermissionAddGroupInput input)
        {
            //var entity = Mapper.Map<PermissionEntity>(input);
            //var id = (await _permissionRepository.Insert(entity)).Id;

            //return id > 0;
            throw new NotImplementedException();
        }

        public async Task<bool> AddMenu(PermissionAddMenuInput input)
        {
            //var entity = Mapper.Map<PermissionEntity>(input);
            //var id = (await _permissionRepository.Insert(entity)).Id;

            //return id > 0;
            throw new NotImplementedException();
        }

        public async Task<bool> AddApi(PermissionAddApiInput input)
        {
            //var entity = Mapper.Map<PermissionEntity>(input);
            //var id = (await _permissionRepository.Insert(entity)).Id;

            //return id > 0;
            throw new NotImplementedException();
        }

       // [Transaction]
        public async Task<bool> AddDot(PermissionAddDotInput input)
        {
            //var entity = Mapper.Map<PermissionEntity>(input);
            //var id = (await _permissionRepository.Insert(entity)).Id;

            //if (input.ApiIds != null && input.ApiIds.Any())
            //{
            //    var permissionApis = input.ApiIds.Select(a => new PermissionApiEntity { PermissionId = id, ApiId = a });
            //    await _permissionApiRepository.Insert(permissionApis);
            //}

            //return id > 0;
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateGroup(PermissionUpdateGroupInput input)
        {
            //var result = false;
            //if (input != null && input.Id > 0)
            //{
            //    var entity = await _permissionRepository.Get(input.Id);
            //    entity = Mapper.Map(input, entity);
            //    result = await _permissionRepository.Update(entity) > 0;
            //}

            //return result;
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateMenu(PermissionUpdateMenuInput input)
        {
            //var result = false;
            //if (input != null && input.Id > 0)
            //{
            //    var entity = await _permissionRepository.Get(input.Id);
            //    entity = Mapper.Map(input, entity);
            //    result = await _permissionRepository.Update(entity) > 0;
            //}

            //return result;
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateApi(PermissionUpdateApiInput input)
        {
            //var result = false;
            //if (input != null && input.Id > 0)
            //{
            //    var entity = await _permissionRepository.Get(input.Id);
            //    entity = Mapper.Map(input, entity);
            //    result = await _permissionRepository.Update(entity) > 0;
            //}

            //return result;
            throw new NotImplementedException();
        }

       // [Transaction]
        public async Task<bool> UpdateDot(PermissionUpdateDotInput input)
        {
            //if (!(input?.Id > 0))
            //{
            //    return false;
            //}

            //var entity = await _permissionRepository.Get(input.Id);
            //if (!(entity?.Id > 0))
            //{
            //    //return ResponseOutput.NotOk("权限点不存在！");
            //    throw new BussinessException(StatusCodes.Status999Falid, "权限点不存在！");
            //}

            //Mapper.Map(input, entity);
            //await _permissionRepository.Update(entity);

            //await _permissionApiRepository.Delete(a => a.PermissionId == entity.Id);

            //if (input.ApiIds != null && input.ApiIds.Any())
            //{
            //    var permissionApis = input.ApiIds.Select(a => new PermissionApiEntity { PermissionId = entity.Id, ApiId = a });
            //    await _permissionApiRepository.Insert(permissionApis);
            //}

            ////清除用户权限缓存
            //await ClearUserPermissions(new List<long> { entity.Id });

            //return true;
            throw new NotImplementedException();
        }

        //[Transaction]
        public async Task<bool> Delete(long id)
        {
            ////递归查询所有权限点
            //var ids = _permissionRepository.Select
            //.Where(a => a.Id == id)
            //.AsTreeCte()
            //.ToList(a => a.Id);

            ////删除权限关联接口
            //await _permissionApiRepository.Delete(a => ids.Contains(a.PermissionId));

            ////删除相关权限
            //await _permissionRepository.Delete(a => ids.Contains(a.Id));

            ////清除用户权限缓存
            //await ClearUserPermissions(ids);

            //return true;
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDelete(long id)
        {
            ////递归查询所有权限点
            //var ids = _permissionRepository.Select
            //.Where(a => a.Id == id)
            //.AsTreeCte()
            //.ToList(a => a.Id);

            ////删除权限
            //await _permissionRepository.SoftDelete(a => ids.Contains(a.Id));

            ////清除用户权限缓存
            //await ClearUserPermissions(ids);

            //return true;
            throw new NotImplementedException();
        }

       // [Transaction]
        public async Task<bool> Assign(PermissionAssignInput input)
        {
            ////分配权限的时候判断角色是否存在
            //var exists = await _roleRepository.Select.DisableGlobalFilter("Tenant").WhereDynamic(input.RoleId).Any();
            //if (!exists)
            //{
            //    //return ResponseOutput.NotOk("该角色不存在或已被删除！");
            //    throw new BussinessException(StatusCodes.Status999Falid, "该角色不存在或已被删除！");
            //}

            ////查询角色权限
            //var permissionIds = await _rolePermissionRepository.Select.Where(d => d.RoleId == input.RoleId).ToList(m => m.PermissionId);

            ////批量删除权限
            //var deleteIds = permissionIds.Where(d => !input.PermissionIds.Contains(d));
            //if (deleteIds.Any())
            //{
            //    await _rolePermissionRepository.Delete(m => m.RoleId == input.RoleId && deleteIds.Contains(m.PermissionId));
            //}

            ////批量插入权限
            //var insertRolePermissions = new List<RolePermissionEntity>();
            //var insertPermissionIds = input.PermissionIds.Where(d => !permissionIds.Contains(d));

            ////防止租户非法授权
            //if (_appConfig.Tenant && User.TenantType == TenantType.Tenant)
            //{
            //    var masterDb = ServiceProvider.GetRequiredService<IFreeSql>();
            //    var tenantPermissionIds = await masterDb.GetRepositoryBase<TenantPermissionEntity>().Select.Where(d => d.TenantId == User.TenantId).ToList(m => m.PermissionId);
            //    insertPermissionIds = insertPermissionIds.Where(d => tenantPermissionIds.Contains(d));
            //}

            //if (insertPermissionIds.Any())
            //{
            //    foreach (var permissionId in insertPermissionIds)
            //    {
            //        insertRolePermissions.Add(new RolePermissionEntity()
            //        {
            //            RoleId = input.RoleId,
            //            PermissionId = permissionId,
            //        });
            //    }
            //    await _rolePermissionRepository.Insert(insertRolePermissions);
            //}

            ////清除角色下关联的用户权限缓存
            //var userIds = await _userRoleRepository.Select.Where(a => a.RoleId == input.RoleId).ToList(a => a.UserId);
            //foreach (var userId in userIds)
            //{
            //    await Cache.Del(string.Format(CacheKey.UserPermissions, userId));
            //}

            //return true;
            throw new NotImplementedException();
        }

       // [Transaction]
        public async Task<bool> SaveTenantPermissions(PermissionSaveTenantPermissionsInput input)
        {
            /* //获得租户db
             var ib = ServiceProvider.GetRequiredService<IdleBus<IFreeSql>>();
             var tenantDb = ib.GetTenantFreeSql(ServiceProvider, input.TenantId);

             //查询租户权限
             var permissionIds = await _tenantPermissionRepository.Select.Where(d => d.TenantId == input.TenantId).ToList(m => m.PermissionId);

             //批量删除租户权限
             var deleteIds = permissionIds.Where(d => !input.PermissionIds.Contains(d));
             if (deleteIds.Any())
             {
                 await _tenantPermissionRepository.Delete(m => m.TenantId == input.TenantId && deleteIds.Contains(m.PermissionId));
                 //删除租户下关联的角色权限
                 await tenantDb.GetRepositoryBase<RolePermissionEntity>().Delete(a => deleteIds.Contains(a.PermissionId));
             }

             //批量插入租户权限
             var tenatPermissions = new List<TenantPermissionEntity>();
             var insertPermissionIds = input.PermissionIds.Where(d => !permissionIds.Contains(d));
             if (insertPermissionIds.Any())
             {
                 foreach (var permissionId in insertPermissionIds)
                 {
                     tenatPermissions.Add(new TenantPermissionEntity()
                     {
                         TenantId = input.TenantId,
                         PermissionId = permissionId,
                     });
                 }
                 await _tenantPermissionRepository.Insert(tenatPermissions);
             }

             //清除租户下所有用户权限缓存
             var userIds = await tenantDb.GetRepositoryBase<UserEntity>().Select.Where(a => a.TenantId == input.TenantId).ToList(a => a.Id);
             if (userIds.Any())
             {
                 foreach (var userId in userIds)
                 {
                     await Cache.Del(string.Format(CacheKey.UserPermissions, userId));
                 }
             }

             return true;*/
            throw new NotImplementedException();
        }

        public async Task<object> GetPermissionList()
        {
            /* var permissions = await _permissionRepository.Select
                 .WhereIf(_appConfig.Tenant && User.TenantType == TenantType.Tenant, a =>
                     _tenantPermissionRepository
                     .Where(b => b.PermissionId == a.Id && b.TenantId == User.TenantId)
                     .Any()
                 )
                 .OrderBy(a => a.ParentId)
                 .OrderBy(a => a.Sort)
                 .ToList(a => new { a.Id, a.ParentId, a.Label, a.Type });

             var apis = permissions
                 .Where(a => a.Type == PermissionType.Dot)
                 .Select(a => new { a.Id, a.ParentId, a.Label });

             var menus = permissions
                 .Where(a => (new[] { PermissionType.Group, PermissionType.Menu }).Contains(a.Type))
                 .Select(a => new
                 {
                     a.Id,
                     a.ParentId,
                     a.Label,
                     Apis = apis.Where(b => b.ParentId == a.Id).Select(b => new { b.Id, b.Label })
                 });

             return menus;*/
          return await _permissionRepository.GetPermissionList();
        }

        public async Task<List<long>> GetRolePermissionList(long roleId = 0)
        {
            /* var permissionIds = await _rolePermissionRepository
                 .Select.Where(d => d.RoleId == roleId)
                 .ToList(a => a.PermissionId);

             return permissionIds;*/
            throw new NotImplementedException();
        }

        public async Task<List<long>> GetTenantPermissionList(long tenantId)
        {
            /*var permissionIds = await _tenantPermissionRepository
                .Select.Where(d => d.TenantId == tenantId)
                .ToList(a => a.PermissionId);

            return permissionIds;*/
            throw new NotImplementedException();
        }
    }
}