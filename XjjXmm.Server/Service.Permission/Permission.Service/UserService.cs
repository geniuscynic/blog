using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Permission.Entity;
using Permission.IRepository;
using Permission.Model;
using Permission.Repository;
using XjjXmm.Core.FrameWork.Common;
using XjjXmm.Core.FrameWork.Interceptor;
using XjjXmm.Core.FrameWork.Mapper;
using XjjXmm.Core.FrameWork.ToolKit;

namespace Permission.Service
{
    public class UserService //: IAccountService
    {

        public IUserRepository UserRepository { get; set; }

        public async Task<BussinessModel<IEnumerable<UserModel>>> GetUser()
        {
            var users = await UserRepository.GetAll();


            return new BussinessModel<IEnumerable<UserModel>>(users.MapTo<User, UserModel>());
        }

        public async Task<BussinessModel<UserModel>> AddUser(AddUserModel userModel)
        {
            var user = userModel.MapTo<AddUserModel, User>();
            user.Id = GuidKit.Get();
            user.CreateBy = "";
            user.CreateTime = DateTime.Now;
            user.UpdatedBy = "";
            user.UpdatedTime = DateTime.Now;
            user.Status = (int)Status.Active;

            var result = await UserRepository.Add(user) > 0 ? "" : "添加失败";


            return new BussinessModel<UserModel>(user.MapTo<User, UserModel>())
            {
                Message = result
            };
        }

        public async Task<BussinessModel<UserModel>> EditUser(EditUserModel userModel)
        {
            var user = userModel.MapTo<EditUserModel, User>();
            user.UpdatedBy = "";
            user.UpdatedTime = DateTime.Now;
            //user.Status = (int)Status.Active;

            var result = await UserRepository.Save(user) > 0 ? "" : "保存失败";


            return new BussinessModel<UserModel>(user.MapTo<User, UserModel>())
            {
                Message = result
            };

        }

        public async Task<BussinessModel<bool>> SetUserStatus(string id, Status status)
        {

            //user.Status = (int)Status.Active;

            var result = await UserRepository.Update<User>(() => new User()
            {
                Status = status.ToInt()
            },
                u => u.Id == id) > 0;



            return new BussinessModel<bool>(result);

        }

        public async Task<BussinessModel<UserModel>> FindUser(LoginModel loginModel)
        {
            var user = await UserRepository.FirstOrDefault(t => t.Account == loginModel.Login && t.Password == loginModel.Password);

            if (user == null)
            {
                return new BussinessModel<UserModel>(null);
            }

            return new BussinessModel<UserModel>(user.MapTo<User, UserModel>());
        }


        //public Task<BussinessModel<RoleModel>> AddRole(RoleModel roleModel)
        //{
        //    var role = roleModel.MapTo<RoleModel, Role>();


        //    if (!role.Id.IsNullOrWhiteSpace())
        //    {
        //        role.Id = GuidKit.Get();
        //        roleModel.Id = role.Id;
        //    }

        //    var result = await UserRepository.Add(user) > 0 ? "" : "添加失败";


        //    return new BussinessModel<UserModel>(userModel)
        //    {
        //        Message = result
        //    };
        //}


    }
}
