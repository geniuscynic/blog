using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;
using XjjXmm.FrameWork.ToolKit;

namespace XjjXmm.Authorize.Service
{
    //public interface IRoleService
    //{
    //    Task<bool> Add(AddRoleModel model);

    //    Task<IEnumerable<RoleModel>> GetRoleByUserId(string userId);
    //}


    [Injection]
    public class RoleService //: IRoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }


        public async Task<bool> Add(AddRoleModel model)
        {
            //var entity = model.MapTo<AddRoleModel, RoleEntity>();
            //entity.Id = GuidKit.Get();


            //var result = await _roleRepository.Add(entity) > 0;



            //return result;
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<RoleSmallDto>> FindByUserId(long userId)
        {
            //var results = await _roleRepository.GetRoleByUserId(userId);
            //return results.MapTo<RoleEntity, RoleModel>();
            var roles = await _roleRepository.FindByUserId(userId);

            return roles.MapTo<RoleEntity, RoleSmallDto>();
        }
    }
}
