using System;
using System.Linq;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using DoCare.Zkzx.Core.FrameWork.Tool.ToolKit;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace XjjXmm.Authorize.Service
{
    [Injection]
    public class UserService// : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly RoleService _roleService;

        public UserService(UserRepository userRepository, RoleService roleService)
        {
            _userRepository = userRepository;
            _roleService = roleService;
        }


        public async Task<bool> AddUser(AddUserModel userModel)
        {
            var user = userModel.MapTo<AddUserModel, UserEntity>();
            user.Id = GuidKit.Get();
            user.CreateBy = "";
            user.CreateTime = DateTime.Now;
            user.UpdatedBy = "";
            user.UpdatedTime = DateTime.Now;
            user.Status = (int)UserStatus.Active;

            var result = await _userRepository.Add(user) > 0;



            return result;
        }

        public async Task<bool> EditUser(EditUserModel userModel)
        {
            var user = userModel.MapTo<EditUserModel, UserEntity>();
            user.UpdatedBy = "";
            user.UpdatedTime = DateTime.Now;
            //user.Status = (int)Status.Active;

            var result = await _userRepository.Save(user) > 0;


            return result;

        }

        // public IUserRepository UserRepository { get; set; }

        // public RoleService RoleService { get; set; }

        public async Task<PageModel<UserModel>> GetUsers(SearchUserModel model)
        {
            //if (pageIndex < 1)
            //{
            //    pageIndex = 1;
            //}

            //if (pageSize < 0)
            //{
            //    pageSize = 10;
            //}

            var result = await _userRepository.GetUsers(model);

            var pageModel = new PageModel<UserModel>()
            {
                Data = result.users.MapTo<UserEntity, UserModel>(),
                Total = result.total,
                Page = model.PageIndex,
                PageSize = model.PageSize
            };

            return pageModel;
        }

        public async Task<UserDetailModel> FindUser(LoginModel loginModel)
        {
            var user = await _userRepository.FirstOrDefault(t => t.Account == loginModel.Login && t.Password == loginModel.Password);

            if (user == null)
            {
                throw BussinessException.CreateException(ExceptionCode.CustomException, "用户名或者密码错误");
            }

            var userModel = user.MapTo<UserEntity, UserDetailModel>();

            var roles = await _roleService.GetRoleByUserId(userModel.Id);

            userModel.Roles = roles.Select(t => t.Id);

            return userModel;


        }

        public async Task<UserDetailModel> FindUser(string id)
        {


            var user = _userRepository.FirstOrDefault(t => t.Id == id).Result;

            if (user == null)
            {
                throw BussinessException.CreateException(ExceptionCode.CustomException, "找不到用户");
            }

            var userModel = user.MapTo<UserEntity, UserDetailModel>();



            var roles = await _roleService.GetRoleByUserId(userModel.Id);

            userModel.Roles = roles.Select(t => t.Id);

            return userModel;
        }


       

        public async Task<bool> SetUserStatus(string id, UserStatus status)
        {

            //user.Status = (int)Status.Active;

            var result = await _userRepository.Update<UserEntity>(() => new UserEntity()
            {
                Status = (int)status
            },
                u => u.Id == id) > 0;



            return result;

        }





    }
}
