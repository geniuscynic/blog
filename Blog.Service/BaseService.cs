using Blog.Common.Extensions.AOP;
using Blog.Core.IService;
using Blog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public abstract class BaseServices<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected abstract IBaseRepository<TEntity> baseRepository { get; set; }//通过在子类的构造函数中注入，这里是基类，不用构造函数

        

        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        public async Task<TEntity> QueryById(object id)
        {
            return await baseRepository.QueryById(id);
        }
    }
}
