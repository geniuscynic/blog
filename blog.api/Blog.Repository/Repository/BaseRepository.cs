using Blog.Core;
using Blog.Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
         private readonly Dbcontext _dbcontext;

        public ISqlSugarClient Db => _dbcontext.Db;

        //protected SimpleClient<TEntity> simpleClient
        //{
        //    get { return DbcContext.GetSimpleClient<TEntity>(); }
        //}

        public BaseRepository(Dbcontext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public async Task<int> Add(TEntity model)
        {
            return await Db.Insertable(model).ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TEntity> AddReturnEntity(TEntity model)
        {
            return await Db.Insertable(model).ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<bool> Add(List<TEntity> models)
        {
            return await Db.Insertable(models.ToArray()).ExecuteCommandAsync() > 0;
        }

        public Task<bool> Edit(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(TEntity model, Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Save(TEntity model)
        {
           return await Db.Saveable(model).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> UpdateColumns(TEntity model, Expression<Func<TEntity, object>> columns)
        {
            return await Db.Updateable<TEntity>(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await Db.Deleteable<TEntity>(model).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteById(object id)
        {
            return await Db.Deleteable<TEntity>(id).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await Db.Deleteable<TEntity>(ids).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Db.Queryable<TEntity>().Where(whereExpression).ToListAsync();
        }

        public async Task<TEntity> FindById(object id)
        {
            return await Db.Queryable<TEntity>().InSingleAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await Db.Queryable<TEntity>().ToListAsync();
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
