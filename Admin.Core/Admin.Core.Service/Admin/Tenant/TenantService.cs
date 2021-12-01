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

namespace Admin.Core.Service.Admin.Tenant
{
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

        public async Task<IResponseOutput> GetAsync(long id)
        {
            var result = await _tenantRepository.GetAsync<TenantGetOutput>(id);
            return ResponseOutput.Ok(result);
        }

        public async Task<IResponseOutput> PageAsync(PageInput<TenantEntity> input)
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

            return ResponseOutput.Ok(data);
        }

        [Transaction]
        public async Task<IResponseOutput> AddAsync(TenantAddInput input)
        {
            var entity = Mapper.Map<TenantEntity>(input);
            var tenant = await _tenantRepository.InsertAsync(entity);

            var tenantId = tenant.Id;

            //����û�
            var pwd = MD5Encrypt.Encrypt32("111111");
            var user = new UserEntity { TenantId = tenantId, UserName = input.Phone, NickName = input.RealName, Password = pwd, Status = 0 };
            await _userRepository.InsertAsync(user);

            //��ӽ�ɫ
            var role = new RoleEntity { TenantId = tenantId, Code = "plat_admin", Name = "ƽ̨����Ա", Enabled = true };
            await _roleRepository.InsertAsync(role);

            //����û���ɫ
            var userRole = new UserRoleEntity() { UserId = user.Id, RoleId = role.Id };
            await _userRoleRepository.InsertAsync(userRole);

            //�����⻧�û��ͽ�ɫ
            tenant.UserId = user.Id;
            tenant.RoleId = role.Id;
            await _tenantRepository.UpdateAsync(tenant);

            return ResponseOutput.Ok();
        }

        public async Task<IResponseOutput> UpdateAsync(TenantUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return ResponseOutput.NotOk();
            }

            var entity = await _tenantRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                return ResponseOutput.NotOk("�⻧�����ڣ�");
            }

            Mapper.Map(input, entity);
            await _tenantRepository.UpdateAsync(entity);
            return ResponseOutput.Ok();
        }

        [Transaction]
        public async Task<IResponseOutput> DeleteAsync(long id)
        {
            //ɾ����ɫȨ��
            await _rolePermissionRepository.Where(a => a.Role.TenantId == id).DisableGlobalFilter("Tenant").ToDelete().ExecuteAffrowsAsync();

            //ɾ���û���ɫ
            await _userRoleRepository.Where(a => a.User.TenantId == id).DisableGlobalFilter("Tenant").ToDelete().ExecuteAffrowsAsync();

            //ɾ���û�
            await _userRepository.Where(a => a.TenantId == id).DisableGlobalFilter("Tenant").ToDelete().ExecuteAffrowsAsync();

            //ɾ����ɫ
            await _roleRepository.Where(a => a.TenantId == id).DisableGlobalFilter("Tenant").ToDelete().ExecuteAffrowsAsync();

            //ɾ���⻧
            await _tenantRepository.DeleteAsync(id);

            return ResponseOutput.Ok();
        }

        [Transaction]
        public async Task<IResponseOutput> SoftDeleteAsync(long id)
        {
            //ɾ���û�
            await _userRepository.SoftDeleteAsync(a => a.TenantId == id, "Tenant");

            //ɾ����ɫ
            await _roleRepository.SoftDeleteAsync(a => a.TenantId == id, "Tenant");

            //ɾ���⻧
            var result = await _tenantRepository.SoftDeleteAsync(id);

            return ResponseOutput.Result(result);
        }

        [Transaction]
        public async Task<IResponseOutput> BatchSoftDeleteAsync(long[] ids)
        {
            //ɾ���û�
            await _userRepository.SoftDeleteAsync(a => ids.Contains(a.TenantId.Value), "Tenant");

            //ɾ����ɫ
            await _roleRepository.SoftDeleteAsync(a => ids.Contains(a.TenantId.Value), "Tenant");

            //ɾ���⻧
            var result = await _tenantRepository.SoftDeleteAsync(ids);

            return ResponseOutput.Result(result);
        }
    }
}