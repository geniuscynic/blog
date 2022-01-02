using Admin.Repository;
using Admin.Repository.Role;
using Admin.Repository.RolePermission;
using Admin.Service.Role.Input;
using Admin.Service.Role.Output;
using System;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.Role
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

        public async Task<RoleGetOutput> Get(long id)
        {
            var result = await _roleRepository.GetById(id);

            var dto = result.MapTo<RoleEntity, RoleGetOutput>();

            //return result;
            return dto;
        }

        public async Task<PageOutput<RoleListOutput>> Page(PageInput<RoleListInput> input)
        {
            //var key = input.Filter?.Name;

            //var list = await _roleRepository.Select
            //.WhereIf(key.NotNull(), a => a.Name.Contains(key))
            //.Count(out var total)
            //.OrderByDescending(true, c => c.Id)
            //.Page(input.CurrentPage, input.PageSize)
            //.ToListAsync<RoleListOutput>();

            //var data = new PageOutput<RoleListOutput>()
            //{
            //    List = list,
            //    Total = total
            //};

            //return data;
            var entity = input.MapTo<PageInput<RoleListInput>, PageInput<RoleEntity>>();
            var result = await _roleRepository.Page(entity);

            var dto = result.Data.MapTo<RoleEntity, RoleListOutput>();

            return new PageOutput<RoleListOutput>
            {
                CurrentPage = input.CurrentPage,
                Total = result.Total,
                PageSize = input.PageSize,
                Data = dto
            };
        }

        public async Task<bool> Add(RoleAddInput input)
        {
            var entity = input.MapTo<RoleAddInput, RoleEntity>();
            base.Fill(entity,FillStatus.Add);
            var result = await _roleRepository.Add(entity);
            
            return result > 0;
            //var id = (await _roleRepository.Insert(entity)).Id;

            //return id > 0;
        }

        public async Task<bool> Update(RoleUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            //var entity = await _roleRepository.Get(input.Id);
            //if (!(entity?.Id > 0))
            //{
            //    //return ResponseOutput.NotOk("角色不存在！");
            //    throw new BussinessException(StatusCodes.Status999Falid, "角色不存在！");
            //}

            var entity = input.MapTo<RoleUpdateInput, RoleEntity>();
            base.Fill(entity, FillStatus.Update);
            return await _roleRepository.Update(entity);

        }

        public async Task<bool> Delete(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = await _roleRepository.Delete(id);
            }

            return result;
            //throw new NotImplementedException();
        }

        public async Task<bool> SoftDelete(long id)
        {
            var result = await _roleRepository.SoftDelete(id);
            await _rolePermissionRepository.Delete(a => a.RoleId == id);
            return result;
        }

        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            var result = await _roleRepository.SoftDelete(ids);
            await _rolePermissionRepository.Delete(a => ids.Contains(a.RoleId));

            return result;
        }
    }
}