using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository
{

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected readonly ISqlSugarClient _context;
        // SimpleClient
        public RepositoryBase(ISqlSugarClient context)
        {
            this._context = context;
        }

        public async Task<long> Add(T entity)
        {
            return await _context.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
        }

        public async Task<List<long>> Add(List<T> entity)
        {
            return await _context.Insertable(entity).ExecuteReturnSnowflakeIdListAsync();
        }

        public async Task<bool> Update(T entity)
        {
            return await _context.Updateable(entity).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Delete(dynamic id)
        {
            return await _context.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> whereExpression)
        {
            return await _context.Deleteable<T>(whereExpression).ExecuteCommandAsync() > 0;
        }



        public async Task<bool> SoftDelete(dynamic id)
        {
            return await _context.Deleteable<T>().In(id).IsLogic().ExecuteCommandAsync("Enabled") > 0;
        }

        public async Task<bool> SoftDelete(dynamic[] id)
        {
            return await _context.Deleteable<T>().In(id).IsLogic().ExecuteCommandAsync("Enabled") > 0;
        }

        public async Task<bool> SoftDelete(Expression<Func<T, bool>> whereExpression)
        {
            return await _context.Deleteable<T>(whereExpression).IsLogic().ExecuteCommandAsync() > 0;
        }


        public async Task<T> GetById(dynamic id)
        {
            return await _context.Queryable<T>().InSingleAsync(id);
        }

        public async Task<T> GetFirst(Expression<Func<T, bool>> whereExpression)
        {

            return await _context.Queryable<T>().Where(whereExpression).FirstAsync();
        }



        public async Task<List<T>> Query()
        {
            return await _context.Queryable<T>().ToListAsync();
        }

        public async Task<List<T>> Query(Expression<Func<T, bool>> whereExpression)
        {
            return await _context.Queryable<T>().Where(whereExpression).ToListAsync();
        }

        public async Task<List<T>> Query(bool whereIf, Expression<Func<T, bool>> whereExpression)
        {

            return await _context.Queryable<T>().WhereIF(whereIf, whereExpression).ToListAsync();

        }

      
    }
}
