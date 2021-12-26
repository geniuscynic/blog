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
    }
}