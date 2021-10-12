using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase
{
    public class Repository<T> : IRepository<T>
    {
        protected readonly DbClient _dbclient;
        //protected Dbclient _dbclient { get; set; }

        public Repository(DbClient dbclient)
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

        public async Task<int> Save<TResult>(T t, Expression<Func<T, TResult>> ignoreColunm)
        {
            return await _dbclient.Saveable(t).IgnoreColumns(ignoreColunm).Execute();
        }

        public async Task<int> Save(IEnumerable<T> t)
        {
            return await _dbclient.Saveable(t).Execute();
        }

        public async Task<int> Save<TResult>(IEnumerable<T> t, Expression<Func<T, TResult>> ignoreColunm)
        {
            return await _dbclient.Saveable(t).IgnoreColumns(ignoreColunm).Execute();
        }

        public async Task<int> Update<TResult>(Expression<Func<TResult>> setColunmExpression, Expression<Func<T, bool>> whereExpression)
        {
            return await _dbclient.Updateable<T>().SetColumns(setColunmExpression).Where(whereExpression).Execute();
        }

        public async Task<int> Delete(Expression<Func<T, bool>> whereExpression)
        {
            return await _dbclient.Deleteable<T>().Where(whereExpression).Execute();
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

            PropertyInfo property = null;
            var colunm = "";
            foreach (var t in typeof(T).GetProperties())
            {
                var customAttribute = t.GetCustomAttribute<ColumnAttribute>();

                if (customAttribute?.IsPrimaryKey == true)
                {
                    property = t;
                    colunm = customAttribute.ColumnName;
                    break;
                    
                }
            }


            //var property = typeof(T).GetProperties().Where(t =>
            //{
            //    var customAttribute = t.GetCustomAttribute<ColumnAttribute>();
            //    return customAttribute?.IsPrimaryKey ?? false;

            //}).SingleOrDefault();

            var sql = "";

            if (property?.PropertyType == typeof(int))
            {
                sql = $"{colunm} = {int.Parse(id)}";
            }
            else if (property?.PropertyType == typeof(string))
            {
                sql = $"{colunm} = '{id}'";
            }
            else
            {
                throw new Exception("不支持的主键类型");
            }

            return await _dbclient.Queryable<T>().Where(sql).ExecuteSingleOrDefault();
        }

        public async Task<IEnumerable<T>> Query<TResult>(Expression<Func<T, TResult>> orderBy, OrderByType orderByType = OrderByType.ASC)
        {
            return await this.Query(null, orderBy, orderByType);
        }

        public async Task<IEnumerable<T>> Query(Expression<Func<T, bool>> whereExpression)
        {
            var queryable = _dbclient.Queryable<T>().Where(whereExpression);
           

            return await queryable.ExecuteQuery();
        }

        public async Task<IEnumerable<T>> Query<TResult>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TResult>> orderBy = null, OrderByType orderByType = OrderByType.ASC)
        {
            var queryable = _dbclient.Queryable<T>();

            if (whereExpression != null)
            {
                queryable = queryable.Where(whereExpression);
            }

            if (orderBy != null)
            {
                switch (orderByType)
                {
                    case OrderByType.ASC:
                        queryable.OrderBy(orderBy);
                        break;
                    case OrderByType.DESC:
                        queryable.OrderByDesc(orderBy);
                        break;
                }
            }

            return await queryable.ExecuteQuery();
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
