using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.IService
{
    /// <summary>
    /// 通用service，实现单个服务的增删改查
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseService<TEntity> where TEntity : class
    {

        /// <summary>
        /// 新增一个entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(TEntity model);


        /// <summary>
        /// 修改 model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Edit(TEntity model);

        /// <summary>
        /// 根据主键删除 id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);

        /// <summary>
        /// 删除entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(TEntity model);


        /// <summary>
        /// 条件删除model
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
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindById(object id);

                               /// <summary>
                               /// 获取所有数据
                               /// </summary>
                               /// <returns></returns>
        Task<List<TEntity>> GetAll();
    }
}
