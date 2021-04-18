using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Extension.Dao.Common;
using DoCare.Extension.Dao.Imp.Command;
using DoCare.Extension.Dao.Interface.Command;
using DoCare.Extension.Dao.Interface.Operate;
using DoCare.Extension.Dao.visitor;

namespace DoCare.Extension.Dao.Imp.Operate
{
    public class Updateable<T> : BaseSqlable<T>, IUpdateable<T>, ISqlBuilder
    {
      


        private readonly IWhereCommand<T> whereCommand;

        private readonly StringBuilder setSql = new StringBuilder();

     


        public Updateable(IDbConnection connection) : base(connection)
        {
            whereCommand = new WhereCommand<T>(_sqlPamater);
        }

        public IUpdateable<T> SetColumns<TResult>(Expression<Func<TResult>> predicate)
        {
            var visitor = new NewObjectExpressionVisitor();
            visitor.Visit(predicate);

            //var dic = (IDictionary<string, object>)_dynamicModel;

            var model = predicate.Compile().Invoke();
            var types = model.GetType();


            visitor.UpdatedFields.ForEach(t =>
            {
                var values = types.GetProperty(t.Parameter)?.GetValue(model);

                setSql.Append($" {t.ColumnName} = {paramterPrefix}{t.Parameter},");

                _sqlPamater[t.Parameter] = values;
            });

            return this;
        }

        public IUpdateable<T> Where(Expression<Func<T, bool>> predicate)
        {
            whereCommand.Where(predicate);

            return this;
        }

        public IUpdateable<T> Where(string whereExpression)
        {
            whereCommand.Where(whereExpression);
            return this;
        }

        public IUpdateable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            whereCommand.Where(whereExpression, predicate);

            return this;
          
        }


        public string Build(bool ignorePrefix = true)
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, _) = DaoHelper.GetMetas(type);

            sql.Append($"update {tableName} set ");


            sql.Append(setSql);

            sql.Remove(sql.Length - 1, 1);

            sql.Append(whereCommand.Build().Replace(DatabaseFactory.ParamterSplit, paramterPrefix));

            
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
