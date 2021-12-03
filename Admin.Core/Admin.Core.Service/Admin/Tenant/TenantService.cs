using Admin.Core.Common.Attributes;
using Admin.Core.Common.Helpers;
using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Repository;
using Admin.Core.Repository.Admin;
using Admin.Core.Service.Admin.Tenant.Input;
using Admin.Core.Service.Admin.Tenant.Output;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Service.Admin.Tenant
{
    [Injection]
    public class TenantService : BaseService, ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepositoryBase<UserRoleEntity> _userRoleRepository;
        private readonly IRepositoryBase<RolePermissionEntity> _rolePermissionRepository;

        public TenantService(
            ITenantRepository tenantRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IRepositoryBase<UserRoleEntity> userRoleRepository,
            IRepositoryBase<RolePermissionEntity> rolePermissionRepository
        )
        {
            _tenantRepository = tenantRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<TenantGetOutput> GetAsync(long id)
        {
            var result = await _tenantRepository.GetAsync<TenantGetOutput>(id);
            return result;
        }

        public async Task<PageOutput<TenantListOutput>> PageAsync(PageInput<TenantEntity> input)
        {
            var key = input.Filter?.Name;

            var list = await _tenantRepository.Select
            .WhereIf(key.NotNull(), a => a.Name.Contains(key))
            .Count(out var total)
            .OrderByDescending(true, c => c.Id)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync<TenantListOutput>();

            var data = new PageOutput<TenantListOutput>()
            {
                List = list,
                Total = total
            };

            return data;
        }

        [Transaction]
        public async Task<bool> AddAsync(TenantAddInput input)
        {
            var entity = Mapper.Map<TenantEntity>(input);
            var tenant = await _tenantRepository.InsertAsync(entity);

            var tenantId = tenant.Id;

            //添加用户
            var pwd = MD5Encrypt.Encrypt32("111111");
            var user = new UserEntity { TenantId = tenantId, UserName = input.Phone, NickName = input.RealName, Password = pwd, Status = 0 };
            await _userRepository.InsertAsync(user);

            //添加角色
            var role = new RoleEntity { TenantId = tenantId, Code = "plat_admin", Name = "平台管理员", Enabled = true };
            await _roleRepository.InsertAsync(role);

            //添加用户角色
            var userRole = new UserRoleEntity() { UserId = user.Id, RoleId = role.Id };
            await _userRoleRepository.InsertAsync(userRole);

            //更新租户用户和角色
            tenant.UserId = user.Id;
            tenant.RoleId = role.Id;
            await _tenantRepository.UpdateAsync(tenant);

            return true;
        }

        public async Task<bool> UpdateAsync(TenantUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            var entity = await _tenantRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                //return ResponseOutput.NotOk("租户不存在！");
                throw new BussinessException(StatusCodes.Status999Falid, "租户不存在！");
            }

            Mapper.Map(input, entity);
            await _tenantRepository.UpdateAsync(entity);
            return true;
        }

        [Transaction]
        public async Task<bool> DeleteAsync(long id)
        {
            //删除角色权限
            await _rolePermissionRepository.Where(a => a.Role.TenantId == id).DisableGlobalFilter("Tenant").ToDelete().ExecuteAffrowsAsync();

            //删除用户角色
            await _userRoleRepository.Where(a => a.User.TenantId == id).DisableGlobalFilter("Tenant").ToDelete().ExecuteAffrowsAsync();

            //删除用户
            await _userRepository.Where(a => a.TenantId == id).DisableGlobalFilter("Tenant").ToDelete().ExecuteAffrowsAsync();

            //删除角色
            await _roleRepository.Where(a => a.TenantId == id).DisableGlobalFilter("Tenant").ToDelete().ExecuteAffrowsAsync();

            //删除租户
            await _tenantRepository.DeleteAsync(id);

            return true;
        }

        [Transaction]
        public async Task<bool> SoftDeleteAsync(long id)
        {
            //删除用户
            await _userRepository.SoftDeleteAsync(a => a.TenantId == id, "Tenant");

            //删除角色
            await _roleRepository.SoftDeleteAsync(a => a.TenantId == id, "Tenant");

            //删除租户
            var result = await _tenantRepository.SoftDeleteAsync(id);

            return result;
        }

        [Transaction]
        public async Task<bool> BatchSoftDeleteAsync(long[] ids)
        {
            //删除用户
            await _userRepository.SoftDeleteAsync(a => ids.Contains(a.TenantId.Value), "Tenant");

            //删除角色
            await _roleRepository.SoftDeleteAsync(a => ids.Contains(a.TenantId.Value), "Tenant");

            //删除租户
            var result = await _tenantRepository.SoftDeleteAsync(ids);

            return result;
        }
    }
}