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
        public Task<bool> AssignMenuPermission(int roleId, List<Menu> menus);

          /// <summary>
          /// 分配按钮权限
          /// </summary>
          /// <returns></returns>
        public Task<bool> AssignButtonPermission(int roleId, List<Button> buttons);

    }
}
