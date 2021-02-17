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
    public class Updateable<T> : IUpdateable<T>
    {
        private readonly IDbConnection _connection;

        private readonly SetColunmExpressionVisitor _visitor = new SetColunmExpressionVisitor();

        private readonly WhereExpressionVisitor _wherevisitor = new WhereExpressionVisitor();

        private T _model;
        public Updateable(IDbConnection connection)
        {
            _connection = connection;

        }

        public IUpdateable<T> SetColumns(Expression<Func<T>> predicate)
        {
            _model = predicate.Compile().Invoke();
            _visitor.Visit(predicate);

            return this;
        }

        public IUpdateable<T> Where(Expression<Func<T, bool>> predicate)
        {
            _wherevisitor.Visit(predicate);

            return this;
        }

        public IUpdateable<T> Where(string whereExpression)
        {
            return this;
        }


        private StringBuilder BuildSql()
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, properties) = DaoHelper.GetMetas(type);

            sql.Append($"update {tableName} set ");


            _visitor.UpdatedFields.ForEach(t =>
            {
                sql.Append($" {t.ColumnName} = @{t.Parameter},");
            });

            sql.Remove(sql.Length - 1, 1);




            return sql;
        }

        public async Task<int> Execute()
        {

            var sql = BuildSql();


            //var result = await _connection.ExecuteAsync(sql.ToString(), _model);

            return 1;
        }


    }
}
