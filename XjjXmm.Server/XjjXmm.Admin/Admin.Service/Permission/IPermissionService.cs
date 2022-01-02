using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;
using Admin.Service.Permission.Input;
using Admin.Service.Permission.Output;
using Admin.Repository.Permission;

namespace Admin.Service.Permission
{
    [ProcessLog]
    public interface IPermissionService
    {
        Task<PermissionEntity> Get(long id);

        Task<PermissionGetGroupOutput> GetGroup(long id);

        Task<PermissionGetMenuOutput> GetMenu(long id);

        Task<PermissionGetApiOutput> GetApi(long id);

        Task<PermissionGetDotOutput> GetDot(long id);

        Task<object> GetPermissionList();

        Task<List<long>> GetRolePermissionList(long roleId);


        Task<List<long>> GetTenantPermissionList(long tenantId);

        Task<List<PermissionListOutput>> GetList(string key, DateTime? start, DateTime? end);

        Task<bool> AddGroup(PermissionAddGroupInput input);

        Task<bool> AddMenu(PermissionAddMenuInput input);

        Task<bool> AddApi(PermissionAddApiInput input);

        Task<bool> AddDot(PermissionAddDotInput input);

        Task<bool> UpdateGroup(PermissionUpdateGroupInput input);

        Task<bool> UpdateMenu(PermissionUpdateMenuInput input);

        Task<bool> UpdateApi(PermissionUpdateApiInput input);

        Task<bool> UpdateDot(PermissionUpdateDotInput input);

        Task<bool> Delete(long id);

        Task<bool> SoftDelete(long id);

        Task<bool> Assign(PermissionAssignInput input);

        Task<bool> SaveTenantPermissions(PermissionSaveTenantPermissionsInput input);
    }
}