using Blog.Core;
using Blog.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Models;
using Dapper.XjjxmmHelper;

namespace Blog.Repository
{
    public class BaseRepository2<TEntity> : IBaseRepository<TEntity> where TEntity : RootEntityTkey<int>, new()
    {
        private readonly XjjxmmContext _context;
        //protected readonly XjjxmmContext _dbcontext;

        //protected ISqlSugarClient DbClient => XjjxmmContext.Db;


        public BaseRepository2(XjjxmmContext context)
        {
            _context = context;

            //this._dbcontext = dbcontext;
        }

        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TEntity> Add(TEntity model)
        {
            var id =  await _context.Insert(model).ExecuteAsync();
            model.Id = id;

            return model;

        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<bool> Add(List<TEntity> models)
        {
            models.ForEach(async t=> t = await Add(t));
            return true;
        }

        public async Task<bool> Edit(TEntity model)
        {
           return await _context.Update(model).ExecuteAsync() > 0;
        }

        public Task<bool> DeleteById<T>(T id)
        {

            throw new NotImplementedException();
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await _context.Delete<TEntity>(model).ExecuteAsync() > 0;
        }

        //public async Task<bool> DeleteById(T id)
        //{
            
        //    return await _context.Delete<TEntity>().Where(t=>t.Id == id).ExecuteAsync() > 0;
        //}

        public async Task<bool> DeleteByIds<T>(T[] ids)
        {
            throw new Exception();
            //return await _context.Delete<TEntity>(ids).ExecuteCommandAsync() > 0;
        }

        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new Exception();
            //return await _context.Query<TEntity>().Where(whereExpression).ToListAsync();
        }

        public async Task<TEntity> QueryById(object id)
        {
            throw new Exception();
            //return await _db.Queryable<TEntity>().InSingleAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            throw new Exception();
            //return await _db.Queryable<TEntity>().ToListAsync();
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
