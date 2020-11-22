using AutoMapper;
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
        protected override IBaseRepository<Category> baseRepository { get; set; }

        public CategoryService(IBaseRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
            //this.baseRepository = repository;
        }

         
    }
}
