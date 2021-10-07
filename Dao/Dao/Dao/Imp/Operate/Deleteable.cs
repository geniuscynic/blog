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
    public class Deleteable<T> : IDeleteable<T>
    {
        private readonly IDbConnection _connection;



        //private readonly WhereExpressionVisitor _wherevisitor = new WhereExpressionVisitor();

        //private readonly StringBuilder _whereCause = new StringBuilder();

        private readonly  IWhereCommand<T> whereCommand;

        private Dictionary<string, object> _sqlPamater = new Dictionary<string, object>();

         

        public Deleteable(IDbConnection connection)
        {
            _connection = connection;

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


        private StringBuilder BuildSql()
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, _) = DaoHelper.GetMetas(type);

            sql.Append($"delete from {tableName}  ");

            //sql.Append(" where ");

            sql.Append(whereCommand.Build());

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
