using Admin.Core.Service.Admin.User;
using Admin.Repository.User;
using Admin.Repository.UserRole;
using Admin.Service.Auth;
using Admin.Service.User.Input;
using Admin.Service.User.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.User
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [Injection]
    public class UserService : BaseService, IUserService
    {
        // private readonly AppConfig _appConfig;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        //  private readonly ITenantRepository _tenantRepository;
        //private readonly IApiRepository _apiRepository;

        // private IRoleRepository _roleRepository => LazyGetRequiredService<IRoleRepository>();

        public UserService(
            // AppConfig appConfig,
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository
        // ITenantRepository tenantRepository,
        //IApiRepository apiRepository
        )
        {
            // _appConfig = appConfig;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            //_tenantRepository = tenantRepository;
            //_apiRepository = apiRepository;
        }

        public async Task<AuthLoginOutput> GetLoginUser(long id)
        {
            //var output = new AuthLoginOutput();
            //var entityDto = await _userRepository.Select.DisableGlobalFilter("Tenant").WhereDynamic(id).ToOneAsync<AuthLoginOutput>();
            //if (_appConfig.Tenant && entityDto?.TenantId.Value > 0)
            //{
            //    var tenant = await _tenantRepository.Select.DisableGlobalFilter("Tenant").WhereDynamic(entityDto.TenantId).ToOne(a => new { a.TenantType, a.DataIsolationType });
            //    if (null != tenant)
            //    {
            //        entityDto.TenantType = tenant.TenantType;
            //        entityDto.DataIsolationType = tenant.DataIsolationType;
            //    }
            //}
            //return entityDto;
            throw new Exception();
        }

        public async Task<object> Get(long id)
        {
            //var entity = await _userRepository.Select
            //.WhereDynamic(id)
            //.IncludeMany(a => a.Roles.Select(b => new RoleEntity { Id = b.Id }))
            //.ToOne();

            //var entityDto = Mapper.Map<UserGetOutput>(entity);

            //var roles = await _roleRepository.Select.ToList(a => new { a.Id, a.Name });

            //return new { Form = entityDto, Select = new { roles } };
            throw new Exception();
        }

        public async Task<object> GetSelect()
        {
            //var roles = await _roleRepository.Select.ToList(a => new { a.Id, a.Name });

            //return new { Select = new { roles } };
            throw new Exception();
        }

        public async Task<UserUpdateBasicInput> GetBasic()
        {
            //if (!(User?.Id > 0))
            //{
            //    throw new BussinessException(StatusCodes.Status999Falid, "未登录！");
            //}

            //var data = await _userRepository.GetAsync<UserUpdateBasicInput>(User.Id);
            //return data;
            throw new Exception();
        }

        public async Task<IList<UserPermissionsOutput>> GetPermissions()
        {
            //var key = string.Format(CacheKey.UserPermissions, User.Id);
            //var result = await Cache.GetOrSet(key, async () =>
            //{
            //    return await _apiRepository
            //    .Where(a => _userRoleRepository.Orm.Select<UserRoleEntity, RolePermissionEntity, PermissionApiEntity>()
            //    .InnerJoin((b, c, d) => b.RoleId == c.RoleId && b.UserId == User.Id)
            //    .InnerJoin((b, c, d) => c.PermissionId == d.PermissionId)
            //    .Where((b, c, d) => d.ApiId == a.Id).Any())
            //    .ToListAsync<UserPermissionsOutput>();
            //});
            //return result;
            throw new Exception();
        }

        public async Task<PageOutput<UserListOutput>> Page(PageInput<UserListInput> input)
        {
            //var list = await _userRepository.Select
            //.WhereDynamicFilter(input.DynamicFilter)
            //.Count(out var total)
            //.OrderByDescending(true, a => a.Id)
            //.IncludeMany(a => a.Roles.Select(b => new RoleEntity { Name = b.Name }))
            //.Page(input.CurrentPage, input.PageSize)
            //.ToList();

            //var data = new PageOutput<UserListOutput>()
            //{
            //    List = Mapper.Map<List<UserListOutput>>(list),
            //    Total = total
            //};

            //return data;

            var dto = input.MapTo<PageInput<UserListInput>, PageInput<UserEntity>>();

            var users = await _userRepository.QueryPage(dto);
            var entity = new PageOutput<UserListOutput>()
            {
                CurrentPage = users.CurrentPage,
                Total = users.Total,
                PageSize = users.PageSize,
                Data = users.Data.MapTo<UserEntity, UserListOutput>()
            };

            return entity;
        }

        // [Transaction]
        public async Task<bool> Add(UserAddInput input)
        {
            //if (input.Password.IsNull())
            //{
            //    input.Password = "111111";
            //}

            //input.Password = MD5Encrypt.Encrypt32(input.Password);

            //var entity = Mapper.Map<UserEntity>(input);
            //var user = await _userRepository.Insert(entity);

            //if (!(user?.Id > 0))
            //{
            //    return false;
            //}

            //if (input.RoleIds != null && input.RoleIds.Any())
            //{
            //    var roles = input.RoleIds.Select(a => new UserRoleEntity { UserId = user.Id, RoleId = a });
            //    await _userRoleRepository.Insert(roles);
            //}

            //return true;

            throw new Exception();
        }

        //[Transaction]
        public async Task<bool> Update(UserUpdateInput input)
        {
            //if (!(input?.Id > 0))
            //{
            //    return false;
            //}

            //var user = await _userRepository.Get(input.Id);
            //if (!(user?.Id > 0))
            //{
            //    //return ResponseOutput.NotOk("用户不存在！");
            //    throw new BussinessException(StatusCodes.Status999Falid, "用户不存在！");
            //}

            //Mapper.Map(input, user);
            //await _userRepository.Update(user);

            //await _userRoleRepository.Delete(a => a.UserId == user.Id);

            //if (input.RoleIds != null && input.RoleIds.Any())
            //{
            //    var roles = input.RoleIds.Select(a => new UserRoleEntity { UserId = user.Id, RoleId = a });
            //    await _userRoleRepository.Insert(roles);
            //}

            //return true;
            //return ResponseOutput.Ok();
            throw new Exception();
        }

        public async Task<bool> UpdateBasic(UserUpdateBasicInput input)
        {
            //var entity = await _userRepository.Get(input.Id);
            //entity = Mapper.Map(input, entity);
            //var result = await _userRepository.Update(entity) > 0;

            ////清除用户缓存
            //await Cache.Del(string.Format(CacheKey.UserInfo, input.Id));

            //return result;
            throw new Exception();
        }

        public async Task<bool> ChangePassword(UserChangePasswordInput input)
        {
            //if (input.ConfirmPassword != input.NewPassword)
            //{
            //    //return ResponseOutput.NotOk("新密码和确认密码不一致！");
            //    throw new BussinessException(StatusCodes.Status999Falid, "新密码和确认密码不一致！");
            //}

            //var entity = await _userRepository.Get(input.Id);
            //var oldPassword = MD5Encrypt.Encrypt32(input.OldPassword);
            //if (oldPassword != entity.Password)
            //{
            //    throw new BussinessException(StatusCodes.Status999Falid, "旧密码不正确！");
            //    //return ResponseOutput.NotOk("旧密码不正确！");
            //}

            //input.Password = MD5Encrypt.Encrypt32(input.NewPassword);

            //entity = Mapper.Map(input, entity);
            //var result = await _userRepository.Update(entity) > 0;

            //return result;
            throw new Exception();
        }

        public async Task<bool> Delete(long id)
        {
            //var result = false;
            //if (id > 0)
            //{
            //    result = await _userRepository.Delete(m => m.Id == id) > 0;
            //}

            //return result;
            throw new Exception();
        }

        //[Transaction]
        public async Task<bool> SoftDelete(long id)
        {
            //var result = await _userRepository.SoftDelete(id);
            //await _userRoleRepository.Delete(a => a.UserId == id);

            //return result;
            throw new Exception();
        }

        //[Transaction]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            //var result = await _userRepository.SoftDelete(ids);
            //await _userRoleRepository.Delete(a => ids.Contains(a.UserId));

            //return result;
            throw new Exception();
        }
    }
}