using System;
using System.Linq;
using System.Threading.Tasks;
using Admin.Repository.Permission;
using Admin.Repository.User;
using Admin.Repository.View;
using Admin.Service.Common;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;
using XjjXmm.FrameWork.ToolKit;
using XjjXmm.FrameWork.ToolKit.Captcha;

namespace Admin.Service.Auth
{
    [Injection]
    public class AuthService : BaseService, IAuthService
    {
      
        private readonly ICaptcha _captcha;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IViewRepository _viewRepository;

        public AuthService(
            ICaptcha captcha ,
            IUserRepository userRepository,
            IPermissionRepository permissionRepository,
            IViewRepository viewRepository

        )
        {
            _captcha = captcha;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _viewRepository = viewRepository;
        }

        public Task<object> GetPassWordEncryptKey()
        {
            //写入Redis
            var guid = GuidKit.Get();
            var key = string.Format(CacheKey.PassWordEncryptKey, guid);
            var encyptKey = StringKit.GenerateRandom(8);
            Cache.Set(key, encyptKey, TimeSpan.FromMinutes(5));
            var data = new { key = guid, encyptKey };

            return Task.FromResult<object>(data);
        }

        public async Task<AuthLoginOutput> Login(AuthLoginInput input)
        {
            var isOK = await _captcha.Check(input.Captcha);

            if (!isOK)
            {
               // throw new BussinessException(StatusCodes.Status999Falid, "验证码输入有误");
            }

            if (!input.PasswordKey.IsNullOrEmpty())
            {
                var passwordEncryptKey = string.Format(CacheKey.PassWordEncryptKey, input.PasswordKey);
                var secretKey = Cache.Get<string>(passwordEncryptKey);
                if (!secretKey.IsNullOrEmpty())
                {
                    
                    input.Password = Encryptions.DesDecrypt(secretKey, input.Password);
                    //await Cache.DelAsync(passwordEncryptKey);
                    Cache.Remove(passwordEncryptKey);
                }
                else
                {
                    // return ResponseOutput.NotOk("解密失败！", 1);
                    throw new BussinessException(StatusCodes.Status999Falid, "解密失败！");
                }
            }

            var user = await _userRepository.GetFirst(t=>t.UserName == input.UserName);

            if (!(user?.Id > 0))
            {
              
                throw new BussinessException(StatusCodes.Status999Falid, "账号输入有误!");
            }

            var password = Encryptions.MD5(input.Password);
            if (user.Password != password)
            {
                throw new BussinessException(StatusCodes.Status999Falid, "密码输入有误!");
            }

                 CurrentUser = user;
            var response =  user.MapTo<UserEntity, AuthLoginOutput>();
             

            return response;
        }

        public async Task<AuthUserInfoOutput> GetUserInfo()
        {
            if (!(UserId > 0))
            {
                //return ResponseOutput.NotOk("未登录！");
                throw new BussinessException(StatusCodes.Status999Falid, "未登录！");
            }

            var authUserInfoOutput = new AuthUserInfoOutput { };

            //var user = _cache.Get<UserEntity>($"{CacheKey.UserInfo}{response.Id}")；
            //用户信息
            authUserInfoOutput.User = CurrentUser.MapTo<UserEntity, AuthUserProfileDto>();

            var res = await _permissionRepository.GetMenuPermissionByUserId(UserId.Value);
            authUserInfoOutput.Menus = res.MapTo<PermissionEntity, AuthUserMenuDto>();

            var permissionDot = await _permissionRepository.GetDotPermissionByUserId(UserId.Value);
            authUserInfoOutput.Permissions = permissionDot.Select(t => t.Code);
           // authUserInfoOutput.Menus.ForEach(m =>   )
            //用户菜单
            //authUserInfoOutput.Menus = await _permissionRepository.Select
            //    .Where(a => new[] { PermissionType.Group, PermissionType.Menu }.Contains(a.Type))
            //    .Where(a =>
            //        _permissionRepository.Orm.Select<RolePermissionEntity>()
            //        .InnerJoin<UserRoleEntity>((b, c) => b.RoleId == c.RoleId && c.UserId == User.Id)
            //        .Where(b => b.PermissionId == a.Id)
            //        .Any()
            //    )
            //    .OrderBy(a => a.ParentId)
            //    .OrderBy(a => a.Sort)
            //    .ToListAsync(a => new AuthUserMenuDto { ViewPath = a.View.Path });

            ////用户权限点
            //authUserInfoOutput.Permissions = await _permissionRepository.Select
            //    .Where(a => a.Type == PermissionType.Dot)
            //    .Where(a =>
            //        _permissionRepository.Orm.Select<RolePermissionEntity>()
            //        .InnerJoin<UserRoleEntity>((b, c) => b.RoleId == c.RoleId && c.UserId == User.Id)
            //        .Where(b => b.PermissionId == a.Id)
            //        .Any()
            //    )
            //    .ToListAsync(a => a.Code);

            return authUserInfoOutput;
        }
    }
}