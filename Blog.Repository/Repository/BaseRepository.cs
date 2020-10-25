using Blog.Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly Dbcontext dbcontext;

        protected ISqlSugarClient db;

        protected SimpleClient<TEntity> simpleClient
        {
            get { return dbcontext.GetSimpleClient<TEntity>();  }
        }

        public BaseRepository(Dbcontext dbcontext)
        {
            
            this.dbcontext = dbcontext;

            this.db = dbcontext.Db;
        }


        public async Task<int> Add(TEntity model)
        {
           return await db.Insertable<TEntity>(model).ExecuteReturnIdentityAsync();
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await db.Deleteable<TEntity>(model).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteById(object id)
        {
            return await db.Deleteable<TEntity>(id).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await db.Deleteable<TEntity>(ids).ExecuteCommandAsync() > 0;
        }
    }
}
