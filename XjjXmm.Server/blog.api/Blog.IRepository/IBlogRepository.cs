using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entity;

namespace Blog.IRepository
{
    public interface IBlogRepository : IRepository<BlogArticle>
    {
        Task<(List<BlogArticle>, int)> GetBlogList(int pageIndex = 1, int pageSize = 20);


        Task<BlogArticle> GetById(int id);
    }
}
