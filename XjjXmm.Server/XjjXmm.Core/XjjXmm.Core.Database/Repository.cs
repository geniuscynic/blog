using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database
{
    public class Repository<T> : IRepository<T>
    {
        protected readonly Dbclient _dbclient;
        //protected Dbclient _dbclient { get; set; }

        public Repository(Dbclient dbclient)
        {
            _dbclient = dbclient;
        }

        public async Task<int> Add(T t)
        {
           return await _dbclient.Insertable(t).Execute();
        }

        public async Task<int> Add(IEnumerable<T> t)
        {
            return await _dbclient.Insertable(t).Execute();
        }

        public async Task<int> Save(T t)
        {
            return await _dbclient.Saveable(t).Execute();
        }

        public async Task<int> Save(IEnumerable<T> t)
        {
            return await _dbclient.Saveable(t).Execute();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbclient.Queryable<T>().ExecuteQuery();
        }

        public async Task<T> Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception("参数不能为空");
            }

            var property = typeof(T).GetProperties().Where(t =>
            {
                var customAttribute = t.GetCustomAttribute<ColumnAttribute>();
                return customAttribute?.IsPrimaryKey ?? false;

            }).SingleOrDefault();

            var sql = "";

            if (property?.PropertyType == typeof(int))
            {
                sql = $"{property?.Name} = {int.Parse(id)}";
            }
            else if (property?.PropertyType == typeof(string))
            {
                sql = $"{property?.Name} = '{id}'";
            }
            else
            {
                throw new Exception("不支持的主键类型");
            }

            return await _dbclient.Queryable<T>(sql).ExecuteSingleOrDefault();
        }

        public async Task<IEnumerable<T>> Query(Expression<Func<T, bool>> whereExpression)
        {
            return await _dbclient.Queryable<T>().Where(whereExpression).ExecuteQuery();
        }

        public async Task<T> SingleOrDefault(Expression<Func<T, bool>> whereExpression)
        {
            return await _dbclient.Queryable<T>().Where(whereExpression).ExecuteSingleOrDefault();
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> whereExpression)
        {
            return await _dbclient.Queryable<T>().Where(whereExpression).ExecuteFirstOrDefault();
        }
    }
}
