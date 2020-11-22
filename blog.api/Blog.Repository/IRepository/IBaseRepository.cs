using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {

        public ISqlSugarClient Db { get;  }
        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(TEntity model);


        Task<bool> Add(List<TEntity> models);

        /// <summary>
        /// 修改 model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Edit(TEntity model);

        Task<bool> DeleteById(object id);

        Task<bool> Delete(TEntity model);

        Task<bool> DeleteByIds(object[] ids);

        Task<List<TEntity>> GetAll();

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);

        Task<TEntity> QueryById(object id);
    }
}
