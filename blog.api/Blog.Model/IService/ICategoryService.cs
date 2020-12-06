using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    /// <summary>
    /// categoru service
    /// </summary>
    public interface ICategoryService : IBaseService<Category>
    {
        Task<Category> Save(Category category);
    }
}
