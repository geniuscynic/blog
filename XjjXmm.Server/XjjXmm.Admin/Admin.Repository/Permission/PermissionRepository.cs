using Admin.Repository.Api.Entity;
using Admin.Repository.PermissionApi;
using Admin.Repository.RolePermission;
using Admin.Repository.UserRole;
using Admin.Repository.View;
using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.Permission
{
    [Injection]
    public class PermissionRepository : RepositoryBase<PermissionEntity>, IPermissionRepository
    {
        public PermissionRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<IEnumerable<PermissionEntity>> GetMenuPermissionByUserId(long userId)
        {
            var res = await _context.Queryable<PermissionEntity>()
                .Where(a => new[] { PermissionType.Group, PermissionType.Menu }.Contains(a.Type))
                .InnerJoin<RolePermissionEntity>((a, rp) => a.Id == rp.PermissionId)
                .InnerJoin<UserRoleEntity>((a, rp, ur) => rp.RoleId == ur.RoleId && ur.UserId == userId)
                .OrderBy(a => a.ParentId)
                .OrderBy(a => a.Sort)
                .Mapper(it => it.View, it => it.ViewId, it => it.View.Id)
                .ToListAsync();

            return res;
        }

        public async Task<IEnumerable<PermissionEntity>> GetDotPermissionByUserId(long userId)
        {
            var res = await _context.Queryable<PermissionEntity>()
                .Where(a => a.Type == PermissionType.Dot)
                .InnerJoin<RolePermissionEntity>((a, rp) => a.Id == rp.PermissionId)
                .InnerJoin<UserRoleEntity>((a, rp, ur) => rp.RoleId == ur.RoleId && ur.UserId == userId)
                .ToListAsync();

            return res;
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
                 .ToList(a => new { a.Id, a.ParentId, a.Label, a.Type });   */

            var permissions = await _context.Queryable<PermissionEntity>().OrderBy(a => a.ParentId)
                 .OrderBy(a => a.Sort).ToListAsync();

             var apis = permissions
                 .Where(a => a.Type == PermissionType.Dot)
                 .ToList();

             var menus = permissions
                 .Where(a => (new[] { PermissionType.Group, PermissionType.Menu }).Contains(a.Type))
                 .Select(a => new
                 {
                     a.Id,
                     a.ParentId,
                     a.Label,
                     Apis = apis.Where(b => b.ParentId == a.Id).Select(b => new { b.Id, b.Label })
                 })
                 .ToList();

             return menus;
          
        }

        public async Task<List<PermissionEntity>> GetList(string key, DateTime? start, DateTime? end)
        {
            if (end.HasValue)
            {
                end = end.Value.AddDays(1);
            }

            var data = await _context.Queryable<PermissionEntity>()
                 .Mapper<PermissionEntity, ApiEntity , PermissionApiEntity>(it => ManyToMany.Config(it.PermissionId, it.ApiId) )
                .WhereIF(!string.IsNullOrEmpty(key), a => a.Path.Contains(key) || a.Label.Contains(key))
                .WhereIF(start.HasValue && end.HasValue, a => start <= a.CreatedTime && a.CreatedTime <= end)
                .OrderBy(a => a.ParentId)
                .OrderBy(a => a.Sort)
                .ToListAsync();
                //.ToList(a => new PermissionListOutput { ApiPaths = string.Join(";", _permissionApiRepository.Where(b => b.PermissionId == a.Id).ToList(b => b.Api.Path)) });

            return data;

           // throw new NotImplementedException();
        }
    }
}