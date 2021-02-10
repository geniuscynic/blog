using AutoMapper;
using Blog.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.IRepository;

namespace Blog.Service
{
    public class CategoryService : BaseServices<Category>, ICategoryService
    {
        //protected override IBaseRepository<Category> _defaultRepository { get; set; }

        public CategoryService(IRepository<Category> defaultRepository, IMapper mapper) : base(defaultRepository, mapper)
        {
            //this._defaultRepository = defaultRepository;
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
