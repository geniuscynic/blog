using AutoMapper;
using Blog.IService;
using Blog.Core.Models;
using Blog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class CategoryService : BaseServices<Category>, ICategoryService
    {
        protected override IBaseRepository<Category> baseRepository { get; set; }

        public CategoryService(IBaseRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
            //this.baseRepository = repository;
        }

        public async Task<Category> Save(Category category)
        {
            if (category.Id == 0)
            {
                var id = await Add(category);
                category.Id = id;

                return category;
            }

            await Edit(category);
            //category.Id = id;

            return category;

        }
    }
}
