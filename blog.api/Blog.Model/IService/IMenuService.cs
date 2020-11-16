using Blog.Core.Models;
using Blog.Core.VeiwModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    /// <summary>
    /// menu service
    /// </summary>
    public interface IMenuService  : IBaseService<Menu>
    {
        /// <summary>
        /// 或许带有上下级关系的所有menu
        /// </summary>
        /// <returns></returns>
        Task<List<Menu>> Get();
    }
}
