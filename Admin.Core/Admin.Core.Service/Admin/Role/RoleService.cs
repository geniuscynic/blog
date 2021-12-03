using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Repository;
using Admin.Core.Repository.Admin;
using Admin.Core.Service.Admin.Role.Input;
using Admin.Core.Service.Admin.Role.Output;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Service.Admin.Role
{
    [Injection]
    public class RoleService : BaseService, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRepositoryBase<RolePermissionEntity> _rolePermissionRepository;

        public RoleService(
            IRoleRepository roleRepository,
            IRepositoryBase<RolePermissionEntity> rolePermissionRepository
        )
        {
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<RoleGetOutput> GetAsync(long id)
        {
            var result = await _roleRepository.GetAsync<RoleGetOutput>(id);
            return result;
        }

        public async Task<PageOutput<RoleListOutput>> PageAsync(PageInput<RoleEntity> input)
        {
            var key = input.Filter?.Name;

            var list = await _roleRepository.Select
            .WhereIf(key.NotNull(), a => a.Name.Contains(key))
            .Count(out var total)
            .OrderByDescending(true, c => c.Id)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync<RoleListOutput>();

            var data = new PageOutput<RoleListOutput>()
            {
                List = list,
                Total = total
            };

            return data;
        }

        public async Task<bool> AddAsync(RoleAddInput input)
        {
            var entity = Mapper.Map<RoleEntity>(input);
            var id = (await _roleRepository.InsertAsync(entity)).Id;

            return id > 0;
        }

        public async Task<bool> UpdateAsync(RoleUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            var entity = await _roleRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                //return ResponseOutput.NotOk("角色不存在！");
                throw new BussinessException(StatusCodes.Status999Falid, "角色不存在！");
            }

            Mapper.Map(input, entity);
            await _roleRepository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = (await _roleRepository.DeleteAsync(m => m.Id == id)) > 0;
            }

            return result;
        }

        public async Task<bool> SoftDeleteAsync(long id)
        {
            var result = await _roleRepository.SoftDeleteAsync(id);
            await _rolePermissionRepository.DeleteAsync(a => a.RoleId == id);

            return result;
        }

        public async Task<bool> BatchSoftDeleteAsync(long[] ids)
        {
            var result = await _roleRepository.SoftDeleteAsync(ids);
            await _rolePermissionRepository.DeleteAsync(a => ids.Contains(a.RoleId));

            return result;
        }
    }
}