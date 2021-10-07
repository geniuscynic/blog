using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using DoCare.Zkzx.Core.FrameWork.Tool.ToolKit;
using log4net.Util;
using Permission.Entity;
using Permission.IRepository;
using Permission.Model;
using Permission.Repository;
using XjjXmm.Core.FrameWork.Interceptor;
using XjjXmm.Core.FrameWork.Mapper;

namespace Permission.Service
{

    public class UserService// : IUserService
    {

        public IUserRepository UserRepository { get; set; }

        public RoleService RoleService { get; set; }

        public async Task<PageModel<UserModel>> GetUsers(string name, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            if (pageSize < 0)
            {
                pageSize = 10;
            }

            var result = await UserRepository.GetUsers(name, pageIndex, pageSize);

            var pageModel = new PageModel<UserModel>()
            {
                Data = result.users.MapTo<UserEntity, UserModel>(),
                Total = result.total,
                Page = pageIndex,
                PageSize = pageSize
            };

            return pageModel;
        }

        public async Task<UserDetailModel> FindUser(LoginModel loginModel)
        {
            var user = await UserRepository.FirstOrDefault(t => t.Account == loginModel.Login && t.Password == loginModel.Password);

            if (user == null)
            {
                throw BussinessException.CreateException(ExceptionCode.CustomException, "用户名或者密码错误");
            }

            var userModel = user.MapTo<UserEntity, UserDetailModel>();

            var roles = await RoleService.GetRoleByUserId(userModel.Id);

            userModel.Roles = roles.Select(t => t.Id);

            return userModel;


        }

        public async Task<UserDetailModel> FindUser(string id)
        {


            var user = UserRepository.FirstOrDefault(t => t.Id == id).Result;

            if (user == null)
            {
                throw BussinessException.CreateException(ExceptionCode.CustomException, "找不到用户");
            }

            var userModel = user.MapTo<UserEntity, UserDetailModel>();



            var roles = await RoleService.GetRoleByUserId(userModel.Id);

            userModel.Roles = roles.Select(t => t.Id);

            return userModel;
        }


        public async Task<bool> AddUser(AddUserModel userModel)
        {
            var user = userModel.MapTo<AddUserModel, UserEntity>();
            user.Id = GuidKit.Get();
            user.CreateBy = "";
            user.CreateTime = DateTime.Now;
            user.UpdatedBy = "";
            user.UpdatedTime = DateTime.Now;
            user.Status = (int)Status.Active;

            var result = await UserRepository.Add(user) > 0;


            return result;
        }

        public async Task<bool> EditUser(EditUserModel userModel)
        {
            var user = userModel.MapTo<EditUserModel, UserEntity>();
            user.UpdatedBy = "";
            user.UpdatedTime = DateTime.Now;
            //user.Status = (int)Status.Active;

            var result = await UserRepository.Save(user) > 0;


            return result;

        }

        public async Task<bool> SetUserStatus(string id, Status status)
        {

            //user.Status = (int)Status.Active;

            var result = await UserRepository.Update<UserEntity>(() => new UserEntity()
            {
                Status = (int)status
            },
                u => u.Id == id) > 0;



            return result;

        }





    }
}
