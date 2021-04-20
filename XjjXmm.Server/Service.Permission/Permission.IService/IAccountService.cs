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
        Task<BussinessModel<UserModel>> FindUserByLoginPassword(LoginModel loginModel);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<BussinessModel<bool>> AddRole(UserModel userModel);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<BussinessModel<bool>> AddUser(UserModel userModel);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<BussinessModel<bool>> EditUser(UserModel userModel);
    }
}
