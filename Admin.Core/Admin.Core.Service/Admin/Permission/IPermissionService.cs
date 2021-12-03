using Admin.Core.Common.Output;
using Admin.Core.Service.Admin.Permission.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.Permission.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.Permission
{
    [ProcessLog]
    public  interface IPermissionService
    {
        Task<PermissionEntity> GetAsync(long id);

        Task<PermissionGetGroupOutput> GetGroupAsync(long id);

        Task<PermissionGetMenuOutput> GetMenuAsync(long id);

        Task<PermissionGetApiOutput> GetApiAsync(long id);

        Task<PermissionGetDotOutput> GetDotAsync(long id);

        Task<object> GetPermissionList();

        Task<List<long>> GetRolePermissionList(long roleId);


        Task<List<long>> GetTenantPermissionList(long tenantId);

        Task<List<PermissionListOutput>> GetListAsync(string key, DateTime? start, DateTime? end);

        Task<bool> AddGroupAsync(PermissionAddGroupInput input);

        Task<bool> AddMenuAsync(PermissionAddMenuInput input);

        Task<bool> AddApiAsync(PermissionAddApiInput input);

        Task<bool> AddDotAsync(PermissionAddDotInput input);

        Task<bool> UpdateGroupAsync(PermissionUpdateGroupInput input);

        Task<bool> UpdateMenuAsync(PermissionUpdateMenuInput input);

        Task<bool> UpdateApiAsync(PermissionUpdateApiInput input);

        Task<bool> UpdateDotAsync(PermissionUpdateDotInput input);

        Task<bool> DeleteAsync(long id);

        Task<bool> SoftDeleteAsync(long id);

        Task<bool> AssignAsync(PermissionAssignInput input);

        Task<bool> SaveTenantPermissionsAsync(PermissionSaveTenantPermissionsInput input);
    }
}