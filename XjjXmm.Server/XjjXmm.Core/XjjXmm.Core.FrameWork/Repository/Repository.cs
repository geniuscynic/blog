using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Org.BouncyCastle.Bcpg;
using XjjXmm.Core.Database;
using XjjXmm.Core.Database.Utility;
using XjjXmm.Core.FrameWork.Common;
using XjjXmm.Core.FrameWork.ToolKit;
using XjjXmm.Core.ToolKit;

namespace XjjXmm.Core.FrameWork.Repository
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

        public async Task<T> Find(string id)
        {
            if (id.IsNullOrWhiteSpace())
            {
                throw new BussinessException(new BussinessException.ErrCode()
                {
                    Code = 0x01,
                    Message = "id 不能为空",
                    Name = "空字符串异常"
                });
            }
            var property = typeof(T).GetProperties().Where(t =>
            {
                var customAttribute = t.GetCustomAttribute<ColumnAttribute>();
                return customAttribute?.IsPrimaryKey?? false;

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
