using Blog.Core.Models;
using Blog.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IService
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

    }
}
