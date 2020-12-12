using Blog.Core.Models;
using Blog.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    /// <summary>
    /// menu service
    /// </summary>
    public interface IApiMethodService : IBaseService<ApiMethod>
    {
        /// <summary>
        /// 获取带有上下级关系的所有menu
        /// </summary>
        /// <returns></returns>
        //Task<List<ApiMethod>> Get(string token);

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="addMenuViewModel"></param>
        /// <returns></returns>
        //Task<int> Add(AddButtonViewModel addMenuViewModel);



    }
}
