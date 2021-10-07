using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.Model.Permission;

namespace Blog.IService
{
    /// <summary>
    /// menu service
    /// </summary>
    public interface IMenuService  : IBaseService<Menu>
    {
        /// <summary>
        /// 获取带有上下级关系的所有menu
        /// </summary>
        /// <returns></returns>
        Task<List<Menu>> GetMenus(string token);

                                                             /// <summary>
                                                             /// 添加菜单
                                                             /// </summary>
                                                             /// <param name="addMenuViewModel"></param>
                                                             /// <returns></returns>
        Task<int> AddMenu(AddMenuViewModel addMenuViewModel);
    }
}
