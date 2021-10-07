using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Dao.Command;
using ConsoleApp1.Dao.Common;
using ConsoleApp1.Dao.Imp.Command;
using ConsoleApp1.Dao.Interface.Command;
using ConsoleApp1.Dao.Interface.Operate;
using ConsoleApp1.Dao.visitor;
using Dapper;

namespace ConsoleApp1.Dao.Imp.Operate
{
    public class Queryable<T> : IXXQueryable<T>
    {
        private readonly IDbConnection _connection;

        private readonly IWhereCommand<T> whereCommand;

        //private readonly WhereExpressionVisitor _wherevisitor = new WhereExpressionVisitor();

        //private readonly StringBuilder _whereCause = new StringBuilder();

        private readonly StringBuilder _selectField = new StringBuilder();

        private Dictionary<string, object> _sqlPamater = new Dictionary<string, object>();

        private string prefix = "";

        public Queryable(IDbConnection connection)
        {
            _connection = connection;

            whereCommand = new WhereCommand<T>(_sqlPamater);

        }

        public IXXQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            whereCommand.Where(predicate);

            return this;

        }

        public IXXQueryable<T> Where(string whereExpression)
        {
            whereCommand.Where(whereExpression);

            return this;
        }

        public IXXQueryable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            whereCommand.Where(whereExpression, predicate);

            return this;
          
        }

        public IQueryCommand<TResult> Select<TResult>(Expression<Func<T,TResult>> predicate)
        {
            

            var visitor = new UpdateExpressionVisitor();
            visitor.Visit(predicate);

            visitor.UpdatedFields.ForEach(t =>
            {
                _selectField.Append($"{t.Prefix}.{t.ColumnName} as {t.Parameter},");

                prefix = t.Prefix;
            });
            _selectField.Remove(_selectField.Length - 1, 1);
            return new SimpleQueryable<TResult>(_connection, BuildSql(), _sqlPamater);
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

            sql.Append(whereCommand.Build());

            return sql;
        }

       


        public async Task<List<T>> ToList()
        {
            var sql = BuildSql();

            Console.WriteLine(sql);

            var result = await _connection.QueryAsync<T>(sql.ToString(), _sqlPamater);

            return result.ToList();
        }
    }
}
