using Blog.Core.IService;
using Blog.Core.Models;
using Blog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Service
{
    public class BlogService : BaseServices<BlogArticle>, IBlogService
    {
        private readonly IBaseRepository<BlogArticle> blogRepository;

        public BlogService(IBaseRepository<BlogArticle> blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        

       
    }
}
