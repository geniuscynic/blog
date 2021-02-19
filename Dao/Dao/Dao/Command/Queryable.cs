using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Dao.Common;
using ConsoleApp1.Dao.Operate;
using ConsoleApp1.Dao.visitor;
using Dapper;

namespace ConsoleApp1.Dao.Command
{
    public class Queryable<T> : IXjjXmmQueryable<T>
    {
        private readonly IDbConnection _connection;



        private readonly WhereExpressionVisitor _wherevisitor = new WhereExpressionVisitor();

        private readonly StringBuilder _whereCause = new StringBuilder();

        private readonly StringBuilder _selectField = new StringBuilder();

        private Dictionary<string, object> _dynamicModel = new Dictionary<string, object>();

        private string prefix = "";

        public Queryable(IDbConnection connection)
        {
            _connection = connection;

        }







        public IXjjXmmQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            _wherevisitor.Visit(predicate);

            _wherevisitor.whereModel.Sql.Append(" and");


            prefix = _wherevisitor.whereModel.Prefix;
            return this;
        }

        public IXjjXmmQueryable<T> Where(string whereExpression)
        {
            _whereCause.Append($" ({whereExpression}) and");

            
            return this;
        }

        public IXjjXmmQueryable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            _whereCause.Append($" ({whereExpression}) and");

            var visitor = new NewObjectExpressionVisitor();
            visitor.Visit(predicate);

            //var dic = (IDictionary<string, object>)_dynamicModel;

            var model = predicate.Compile().Invoke();
            var types = model.GetType();

            

            visitor.UpdatedFields.ForEach(t =>
            {
                var values = types.GetProperty(t.Parameter)?.GetValue(model);

                _dynamicModel[t.Parameter] = values;
            });

            return this;
          
        }

        public IQueryOperate<TResult> Select<TResult>(Expression<Func<T,TResult>> predicate)
        {
            

            var visitor = new UpdateExpressionVisitor();
            visitor.Visit(predicate);

            visitor.UpdatedFields.ForEach(t =>
            {
                _selectField.Append($"{t.Prefix}.{t.ColumnName} as {t.Parameter},");

                prefix = t.Prefix;
            });
            _selectField.Remove(_selectField.Length - 1, 1);
            return new SimpleQueryable<TResult>(_connection, BuildSql(), _dynamicModel);
        }


        private StringBuilder BuildSql()
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, _) = DaoHelper.GetMetas(type);

            var selectSql = "*";
            if (_selectField.Length > 0)
            {
                selectSql = _selectField.ToString();
            }


            sql.Append($"select {selectSql} from {tableName} {prefix} ");

            sql.Append(" where ");

            sql.Append(_whereCause);

            sql.Append(_wherevisitor.whereModel.Sql);
            sql.Remove(sql.Length - 3, 3);

            foreach (var keyValuePair in _wherevisitor.whereModel.Parameter)
            {
                _dynamicModel[keyValuePair.Key] = keyValuePair.Value;
            }
            return sql;
        }

       


        public async Task<List<T>> ToList()
        {
            var sql = BuildSql();

            Console.WriteLine(sql);

            var result = await _connection.QueryAsync<T>(sql.ToString(), _dynamicModel);

            return result.ToList();
        }
    }
}
