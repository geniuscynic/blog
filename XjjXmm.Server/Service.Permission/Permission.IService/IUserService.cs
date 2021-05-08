using System.Collections.Generic;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using Permission.Model;


namespace Permission.IService
{
    /// <summary>
    /// menu service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///  用户列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<BussinessModel<PageModel<UserModel>>> GetUsers(string name, int pageIndex, int pageSize);

        /// <summary>
        /// 登入验证
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<BussinessModel<UserDetailModel>> FindUser(LoginModel loginModel);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BussinessModel<UserDetailModel>> FindUser(string id);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<BussinessModel<string>> AddUser(AddUserModel userModel);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<BussinessModel<bool>> EditUser(EditUserModel userModel);

        /// <summary>
        ///  设置用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<BussinessModel<bool>> SetUserStatus(string id, Status status);

       
    }
}
