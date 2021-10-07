using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.Model.Permission;

namespace Blog.IService
{
    /// <summary>
    /// user service
    /// </summary>
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// 登入验证
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        Task<string> Login(LoginViewModel loginViewModel);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="addUserViewModel"></param>
        /// <returns></returns>
        public Task<User> Add(AddUserViewModel addUserViewModel);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="addUserViewModel"></param>
        /// <returns></returns>
        public Task<User> Edit(AddUserViewModel addUserViewModel);

        /// <summary>
        /// 获取所有user
        /// </summary>
        /// <returns></returns>
        public Task<List<AddUserViewModel>> GetUsers();
    }
}
