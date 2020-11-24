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
        protected readonly Dbcontext dbcontext;

        public ISqlSugarClient Db
        {
            get { return dbcontext.Db; }
        }

        protected SimpleClient<TEntity> simpleClient
        {
            get { return dbcontext.GetSimpleClient<TEntity>(); }
        }

        public BaseRepository(Dbcontext dbcontext)
        {

            this.dbcontext = dbcontext;


        }

        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(TEntity model)
        {
            return await Db.Insertable(model).ExecuteReturnIdentityAsync();
        }


       

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<bool> Add(List<TEntity> models)
        {
            return await Db.Insertable(models.ToArray()).ExecuteCommandIdentityIntoEntityAsync();
        }

        public async Task<bool> Edit(TEntity model)
        {
           return await Db.Saveable(model).ExecuteCommandAsync() > 0;
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

        public async Task<TEntity> QueryById(object id)
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
