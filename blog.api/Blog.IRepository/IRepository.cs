using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.IRepository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {

        Task<int> Add(TEntity model);

        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TEntity> AddReturnEntity(TEntity model);

        Task<bool> Add(List<TEntity> models);

        /// <summary>
        /// 修改 model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Edit(TEntity model);

        /// <summary>
        /// 修改 model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Edit(TEntity model, Expression<Func<TEntity, bool>> whereExpression);



        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(TEntity model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteByIds(object[] ids);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAll();


        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);

        Task<TEntity> FindById(object id);

        Task<int> Count(Expression<Func<TEntity, bool>> whereExpression);
    }
}
