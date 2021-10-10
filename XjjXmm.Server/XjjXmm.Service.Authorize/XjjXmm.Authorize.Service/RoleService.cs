using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace XjjXmm.Authorize.Service
{
    [Injection]
    public class RoleService //: IRoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

      
        public async Task<IEnumerable<RoleModel>> GetRoleByUserId(string userId)
        {
            var results = await _roleRepository.GetRoleByUserId(userId);
            return results.MapTo<RoleEntity, RoleModel>();
        }
    }
}
