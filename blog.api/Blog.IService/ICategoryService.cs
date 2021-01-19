using System.Threading.Tasks;
using Blog.Entity;

namespace Blog.IService
{
    /// <summary>
    /// categoru service
    /// </summary>
    public interface ICategoryService : IBaseService<Category>
    {
        Task<Category> Save(Category category);
    }
}
