using AutoMapper;
using Blog.Common.Extensions.AOP;
using Blog.IService;
using Blog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public abstract class BaseServices<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected readonly IMapper mapper;

        public BaseServices(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            this.baseRepository = baseRepository;
            this.mapper = mapper;
        }


        protected abstract IBaseRepository<TEntity> baseRepository { get; set; }//通过在子类的构造函数中注入，这里是基类，不用构造函数

        

        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(TEntity model)
        {
            await baseRepository.Add(model);

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Edit(TEntity model)
        {
            return await baseRepository.Edit(model);
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

        public async Task<List<TEntity>> GetAll()
        {
            return await baseRepository.GetAll();
        }

        public async Task<TEntity> FindById(object id)
        {
            return await baseRepository.FindById(id);
        }
    }
}
