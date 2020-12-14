using Blog.Core;
using Blog.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class BaseRepository2<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly ISqlSugarClient _db;
        //protected readonly Dbcontext _dbcontext;

        //protected ISqlSugarClient DbClient => Dbcontext.Db;


        public BaseRepository2(ISqlSugarClient sqlSugarClient)
        {
            _db = sqlSugarClient;

            //this._dbcontext = dbcontext;
        }

        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TEntity> Add(TEntity model)
        {
            return await _db.Insertable(model).ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<bool> Add(List<TEntity> models)
        {
            return await _db.Insertable(models.ToArray()).ExecuteCommandIdentityIntoEntityAsync();
        }

        public async Task<bool> Edit(TEntity model)
        {
           return await _db.Saveable(model).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await _db.Deleteable<TEntity>(model).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteById<T>(T id)
        {
            
            return await _db.Deleteable<TEntity>(id).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteByIds<T>(T[] ids)
        {
            return await _db.Deleteable<TEntity>(ids).ExecuteCommandAsync() > 0;
        }

        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>().Where(whereExpression).ToListAsync();
        }

        public async Task<TEntity> QueryById(object id)
        {
            return await _db.Queryable<TEntity>().InSingleAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _db.Queryable<TEntity>().ToListAsync();
        }

       


       

       

        /*public async Task<PageModel<TEntity>> QueryPage<T1, T2, TEntity>(
            Expression<Func<T1, T2, object[]>> joinExpression, 
            int pageIndex = 1, int pageSize = 20)
        {
            RefAsync<int> total = 0;
            var result = await Db.Queryable<T1, T2>(joinExpression).
                .ToPageListAsync(pageIndex, pageSize, total);

            int pageCount = (Math.Ceiling(total.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();

            return new PageModel<TEntity>
            {
                Data = result,
                DataCount = total.Value,
                Page = pageIndex,
                PageSize = pageSize,
                PageCount = pageCount
            }
        }*/
    }
}
