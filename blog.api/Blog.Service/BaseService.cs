using AutoMapper;
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
        protected readonly IMapper _mapper;

        protected readonly IBaseRepository<TEntity> _repository;

        protected BaseServices(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }


        //protected abstract IBaseRepository<TEntity> _repository { get; private set; }//通过在子类的构造函数中注入，这里是基类，不用构造函数

        

        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(TEntity model)
        {
            return await _repository.Add(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Edit(TEntity model)
        {
            return await _repository.Edit(model);
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await _repository.Delete(model);
        }

        public async Task<bool> DeleteById(object id)
        {
            return await _repository.DeleteById(id);
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await _repository.DeleteById(ids);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TEntity> FindById(object id)
        {
            return await _repository.FindById(id);
        }
    }
}
