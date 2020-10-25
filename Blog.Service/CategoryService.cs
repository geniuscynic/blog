using Blog.Core.IService;
using Blog.Core.Models;
using Blog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Service
{
    public class CategoryService : BaseServices<Category>, ICategoryService
    {
        private readonly IBaseRepository<Category> repository;

        public CategoryService(IBaseRepository<Category> repository)
        {
            this.repository = repository;
        }

        

       
    }
}
