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
    public class Queryable<T>
    {
        private readonly SqlConnection _connection;

        //private readonly List<WhereExpression> _whereExpressionList = new List<WhereExpression>();
        //private readonly List<WhereExpression> _selectExpressionList = new List<WhereExpression>();

        private StringBuilder sql = new StringBuilder();
        private readonly WhereExpressionVisitor _whereVisitor;

        public Queryable(SqlConnection connection) : this(connection, new StringBuilder(), new WhereExpressionVisitor())
        {
            
        }

        public Queryable(SqlConnection connection, StringBuilder sql, WhereExpressionVisitor _whereVisitor)
        {
            this.sql = sql;
            this._whereVisitor = _whereVisitor;
            _connection = connection;
        }


        public Queryable<T> Where(Expression<Func<T, bool>> predicate)
        {

            _whereVisitor.Run(predicate);

            return this;
        }

        public Queryable<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate)
        {
            var selectVisitor = new SelectExpressionVisitor();

            selectVisitor.Run(predicate);

            if (sql.Length == 0)
            {
                sql.Append(selectVisitor.Result.Sql);
                sql.Append(typeof(T).Name);
                sql.Append($" {selectVisitor.Result.TableName}");
            }
            else
            {
                var temp = new StringBuilder();
                temp.Append(selectVisitor.Result.Sql);

                temp.Append("(");
                temp.Append(sql);
                temp.Append(")");

                temp.Append($" {selectVisitor.Result.TableName}");

                sql = temp;

            }

            sql.Append(_whereVisitor.Result.Sql);

            //_whereVisitor.Result.Start++;
            _whereVisitor.Result.Sql.Clear();

            //_whereVisitor = new WhereExpressionVisitor(_whereVisitor.Result);

            
            return new Queryable<TResult>(_connection, sql, _whereVisitor);
        }

        //public Queryable<TResult> Select<TResult>(WhereExpression<Func<T, TResult>> predicate)
        //{
        //    var whereSql = BuildWhere();



        //    vistor.Visit(predicate);

        //    //var name = typeof(T).Name;
        //    var oName = (predicate as LambdaExpression).Parameters[0].Name;

        //    sql = sql switch
        //    {
        //        "" => $"select {vistor.Sql} from {typeof(T).Name} {oName} {whereSql}",
        //        _ => $"select {vistor.Sql} from ({sql}) {oName} {whereSql}"
        //    };

        //    return new Queryable<TResult>(_connection, sql);
        //}


        public List<T> ToList()
        {
            var sql = Build();

            //var result = _connection.Query<T>(this.sql).ToList();

            return null;
        }

        public string Build()
        {
   

            return sql.ToString();


        }
    }
}
