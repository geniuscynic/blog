using AutoMapper;
using Blog.IService;
using Blog.IRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public abstract class BaseServices<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected readonly IMapper _mapper;

        protected readonly IRepository<TEntity> _defaultRepository;

        protected BaseServices(IRepository<TEntity> defaultRepository, IMapper mapper)
        {
            this._defaultRepository = defaultRepository;
            this._mapper = mapper;
        }


        //protected abstract IBaseRepository<TEntity> _defaultRepository { get; private set; }//通过在子类的构造函数中注入，这里是基类，不用构造函数

        

        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(TEntity model)
        {
            return await _defaultRepository.Add(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Edit(TEntity model)
        {
            return await _defaultRepository.Edit(model);
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await _defaultRepository.Delete(model);
        }

        public async Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _defaultRepository.Delete(whereExpression);
        }

        public async Task<bool> DeleteById(object id)
        {
            return await _defaultRepository.DeleteById(id);
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await _defaultRepository.DeleteById(ids);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _defaultRepository.GetAll();
        }

        public async Task<TEntity> FindById(object id)
        {
            return await _defaultRepository.FindById(id);
        }
    }
}
