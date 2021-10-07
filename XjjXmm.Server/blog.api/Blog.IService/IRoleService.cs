using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Entity;

namespace Blog.IService
{
    /// <summary>
    /// role service
    /// </summary>
    public interface IRoleService : IBaseService<Role>
    {
        /// <summary>
        /// 分配 菜单权限
        /// </summary>
        /// <returns></returns>
        public Task<bool> AssignMenuPermission(int roleId, List<int> menus);

          /// <summary>
          /// 分配按钮权限
          /// </summary>
          /// <returns></returns>
        public Task<bool> AssignButtonPermission(int roleId, List<int> buttons);


          /// <summary>
          /// 分配接口权限
          /// </summary>
          /// <returns></returns>
          public Task<bool> AssignApiMethodPermission(int roleId, List<int> apis);


        /// <summary>
        /// 获取role 对应的权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Task<List<MenuPermission>> GetMenusByRole(int roleId);

          /// <summary>
          /// 获取button 对应的权限
          /// </summary>
          /// <param name="roleId"></param>
          /// <returns></returns>
        public Task<List<ButtonPermission>> GetButtonByRole(int roleId);

          /// <summary>
          /// 获取api method 对应的权限
          /// </summary>
          /// <param name="roleId"></param>
          /// <returns></returns>
          public Task<List<ApiMethodPermission>> GetApiMethodByRole(int roleId);

         /// <summary>
         /// 判断是否有权限
         /// </summary>
         /// <param name="roleCode"></param>
         /// <param name="route"></param>
         /// <returns></returns>
          public Task<bool> HasApiMethodPermission(List<string> roleCode, string route, string httpMethod);

    }
}
