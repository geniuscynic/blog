using Blog.Core.Models;
using Blog.Core.ViewModels;
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
        Task<int> Save(PostBlogViewModel blogViewModel);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PageModel<ListBlogViewModel>> GetBlogList(int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取blog 详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostBlogViewModel> Get(int id);    
    }
}
