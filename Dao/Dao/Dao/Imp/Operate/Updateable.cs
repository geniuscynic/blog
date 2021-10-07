using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Dao.Common;
using ConsoleApp1.Dao.Imp.Command;
using ConsoleApp1.Dao.Interface.Command;
using ConsoleApp1.Dao.Interface.Operate;
using ConsoleApp1.Dao.visitor;
using Dapper;

namespace ConsoleApp1.Dao.Imp.Operate
{
    public class Updateable<T> : IUpdateable<T>
    {
        private readonly IDbConnection _connection;



        //private readonly WhereExpressionVisitor _wherevisitor = new WhereExpressionVisitor();

        //private readonly StringBuilder _whereCause = new StringBuilder();

        private readonly IWhereCommand<T> whereCommand;

        private readonly StringBuilder setSql = new StringBuilder();

     

        private readonly Dictionary<string, object> _sqlPamater = new Dictionary<string, object>();

        public Updateable(IDbConnection connection)
        {
            _connection = connection;

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

                setSql.Append($" {t.ColumnName} = @{t.Parameter},");

                _dynamicModel[t.Parameter] = values;
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


        private StringBuilder BuildSql()
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, _) = DaoHelper.GetMetas(type);

            sql.Append($"update {tableName} set ");


            sql.Append(setSql);

            sql.Remove(sql.Length - 1, 1);

            sql.Append(whereCommand.Build());

            //sql.Append(" where ");

            //sql.Append(_whereCause);

            //sql.Remove(sql.Length - 3, 3);


            return sql;
        }

        public async Task<int> Execute()
        {

            var sql = BuildSql();


            var result = await _connection.ExecuteAsync(sql.ToString(), _sqlPamater);

            return 1;
        }


    }
}
