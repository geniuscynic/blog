using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    /// <summary>
    /// blog service
    /// </summary>
    public interface IBlogService  : IBaseService<BlogArticle>
    {
        /// <summary>
        /// 新增blog
        /// </summary>
        /// <param name="blogViewModel"></param>
        /// <returns></returns>
        Task<int> Add(PostBlogViewModel blogViewModel);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<BlogArticle>> QueryPage(int pageIndex = 1, int pageSize = 20);
    }
}
