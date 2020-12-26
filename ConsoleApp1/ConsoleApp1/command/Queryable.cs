using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    public class Queryable3<T>
    {
        private readonly SqlConnection _connection;

        private readonly List<Expression> _whereExpressionList = new List<Expression>();
        //private readonly List<Expression> _selectExpressionList = new List<Expression>();

        private string sql = "";

        public Queryable3(SqlConnection connection) : this(connection, "")
        {
            
        }

        public Queryable3(SqlConnection connection, string sql)
        {
            this.sql = sql;
            _connection = connection;
        }


        public Queryable3<T> Where(Expression<Func<T, bool>> predicate)
        {

            _whereExpressionList.Add(predicate);
            return this;
        }

        public Queryable3<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate)
        {
            var whereSql = BuildWhere();


            var vistor = new SelectExpressionVisitor3();
            vistor.Visit(predicate);

            //var name = typeof(T).Name;
            var oName = (predicate as LambdaExpression).Parameters[0].Name;

            sql = sql switch
            {
                "" => $"select {vistor.Sql} from {typeof(T).Name} {oName} {whereSql}",
                _ => $"select {vistor.Sql} from ({sql}) {oName} {whereSql}"
            };

            return new Queryable<TResult>(_connection, sql);
        }

        private string BuildWhere()
        {
            var sqls = new List<string>();

            _whereExpressionList.ForEach(t =>
            {
                var vistor = new WhereExpressionVisitor3();
                vistor.Visit(t);

                sqls.Add($"({vistor.Sql})");
            });


            _whereExpressionList.Clear();

            return sqls.Count switch
            {
                0 => string.Join(" and ", sqls),
                _ => " where " + string.Join(" and ", sqls)
            };
        }


        public List<T> ToList()
        {
            var sql = Build();

            //var result = _connection.Query<T>(this.sql).ToList();

            return null;
        }

        public string Build()
        {


            return sql;


        }
    }
}
