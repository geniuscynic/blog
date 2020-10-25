using Blog.Common.Extensions.AOP;
using Blog.Core.IService;
using Blog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BaseServices<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        public IBaseRepository<TEntity> baseRepository;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        //public BlogService(IBlogRepository blogRepository)
        //{
        //    this.blogRepository = blogRepository;
        //}

        public async Task<int> Add(TEntity model)
        {
            return await baseRepository.Add(model);
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await baseRepository.Delete(model);
        }

        public async Task<bool> DeleteById(object id)
        {
            return await baseRepository.DeleteById(id);
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await baseRepository.DeleteById(ids);
        }
    }
}
