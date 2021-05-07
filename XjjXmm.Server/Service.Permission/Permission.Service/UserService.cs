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
using Permission.IService;
using Permission.Model;
using Permission.Repository;
using XjjXmm.Core.FrameWork.Interceptor;
using XjjXmm.Core.FrameWork.Mapper;

namespace Permission.Service
{
    
    public class UserService : IUserService
    {

        public IUserRepository UserRepository { get; set; }

        public IRoleService RoleService { get; set; }

        public async Task<BussinessModel<PageModel<UserModel>>> GetUsers(string name, int pageIndex, int pageSize)
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

            return new BussinessModel<PageModel<UserModel>>(pageModel);
        }

        public async Task<BussinessModel<UserDetailModel>> FindUser(LoginModel loginModel)
        {
            var user = await UserRepository.FirstOrDefault(t => t.Account == loginModel.Login && t.Password == loginModel.Password);

            if (user == null)
            {
                return new BussinessModel<UserDetailModel>(null)
                {
                    Success = false,
                    Message = "用户名或者密码错误"
                };
            }

            var userModel = user.MapTo<UserEntity, UserDetailModel>();

            var roles = await RoleService.GetRoleByUserId(userModel.Id);

            userModel.Roles = roles.Response.Select(t => t.Id);

            return new BussinessModel<UserDetailModel>(userModel);

           
        }

        public async Task<BussinessModel<UserDetailModel>> FindUser(string id)
        {
           

            var user = UserRepository.FirstOrDefault(t => t.Id == id).Result;

            if (user == null)
            {
                return new BussinessModel<UserDetailModel>(null)
                {
                    Success = false,
                    Message = "找不到用户"
                };
            }

            var userModel = user.MapTo<UserEntity, UserDetailModel>();


           
            var roles = await RoleService.GetRoleByUserId(userModel.Id);

            userModel.Roles = roles.Response.Select(t=>t.Id);
          
            return new BussinessModel<UserDetailModel>(userModel);
        }

        
        public async Task<BussinessModel<string>> AddUser(AddUserModel userModel)
        {
            var user = userModel.MapTo<AddUserModel, UserEntity>();
            user.Id = GuidKit.Get();
            user.CreateBy = "";
            user.CreateTime = DateTime.Now;
            user.UpdatedBy = "";
            user.UpdatedTime = DateTime.Now;
            user.Status = (int)Status.Active;

            var result = await UserRepository.Add(user) > 0 ? "" : "添加失败";


            return new BussinessModel<string>(user.Id)
            {
                Message = result,
                Success = result == ""

            };
        }

        public async Task<BussinessModel<bool>> EditUser(EditUserModel userModel)
        {
            var user = userModel.MapTo<EditUserModel, UserEntity>();
            user.UpdatedBy = "";
            user.UpdatedTime = DateTime.Now;
            //user.Status = (int)Status.Active;

            var result = await UserRepository.Save(user) > 0 ? "" : "保存失败";


            return new BussinessModel<bool>(result =="")
            {
                Message = result ,
                Success = result == ""
            };

        }

        public async Task<BussinessModel<bool>> SetUserStatus(string id, Status status)
        {

            //user.Status = (int)Status.Active;

            var result = await UserRepository.Update<UserEntity>(() => new UserEntity()
            {
                Status = (int)status
            },
                u => u.Id == id) > 0;



            return new BussinessModel<bool>(result)
            {
                Success = result
            };

        }

        



    }
}
