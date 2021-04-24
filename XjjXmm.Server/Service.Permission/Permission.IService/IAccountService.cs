using System.Collections.Generic;
using System.Threading.Tasks;
using Permission.Model;
using XjjXmm.Core.FrameWork.Common;

namespace Permission.IService
{
    /// <summary>
    /// menu service
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 登入验证
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<BussinessModel<UserModel>> FindUser(LoginModel loginModel);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<BussinessModel<UserModel>> AddUser(AddUserModel userModel);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<BussinessModel<UserModel>> EditUser(EditUserModel userModel);


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<BussinessModel<RoleModel>> AddRole(RoleModel userModel);
    }
}
