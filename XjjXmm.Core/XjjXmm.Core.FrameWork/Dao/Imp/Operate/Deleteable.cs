using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Dao.Common;
using Dapper;
using DoCare.Extension.Dao.Common;
using DoCare.Extension.Dao.Imp.Command;
using DoCare.Extension.Dao.Interface.Command;
using DoCare.Extension.Dao.Interface.Operate;

namespace DoCare.Extension.Dao.Imp.Operate
{
    public class Deleteable<T> : BaseSqlable<T>, IDeleteable<T>, ISqlBuilder
    {
        private readonly  IWhereCommand<T> whereCommand;

      

        public Deleteable(IDbConnection connection) : base(connection)
        {
            whereCommand = new WhereCommand<T>(_sqlPamater);
        }

        public IDeleteable<T> Where(Expression<Func<T, bool>> predicate)
        {
            whereCommand.Where(predicate);

            return this;
        }

        public IDeleteable<T> Where(string whereExpression)
        {
            whereCommand.Where(whereExpression);

            return this;
        }

        public IDeleteable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            whereCommand.Where(whereExpression, predicate);

            return this;
          
        }


        public string Build(bool ignorePrefix = true)
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, _) = DaoHelper.GetMetas(type);

            sql.Append($"delete from {tableName}  ");

            //sql.Append(" where ");

            sql.Append(whereCommand.Build().Replace(DatabaseFactory.ParamterSplit, paramterPrefix));

            //sql.Remove(sql.Length - 3, 3);


            return sql.ToString();
        }

        public async Task<int> Execute()
        {
            var sql = Build();

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.ExecuteAsync(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }

        }


       
    }
}
