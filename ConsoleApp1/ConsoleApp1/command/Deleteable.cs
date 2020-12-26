using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Deleteable<T>
    {
        private readonly SqlConnection _connection;
        private readonly T _model;

        private readonly List<Expression> _whereExpressionList = new List<Expression>();
        private int index = 0;
        Dictionary<string, object> dict = new Dictionary<string, object>();

        public Deleteable(SqlConnection connection)
        {
            _connection = connection;
          
        }


        public Deleteable(SqlConnection connection, T model)
        {
            _connection = connection;
            _model = model;
        }



        public Deleteable3<T> Where(Expression<Func<T, bool>> predicate)
        {
            _whereExpressionList.Add(predicate);
            return this;
        }

        private string BuildWhere()
        {
            var sqls = new List<string>();

            _whereExpressionList.ForEach(t =>
            {
                var vistor = new WhereExpressionVisitor2(index);
                vistor.Visit(t);
                index = vistor.Index + 1;

                sqls.Add($"({vistor.Sql})");

                foreach (var keyValuePair in vistor.dict)
                {
                    dict.Add(keyValuePair.Key, keyValuePair.Value);
                }
            });


            _whereExpressionList.Clear();

            return sqls.Count switch
            {
                0 => string.Join(" and ", sqls),
                _ => " where " + string.Join(" and ", sqls)
            };
        }

        private StringBuilder buildSql()
        {
            var sql = new StringBuilder();

            sql.Append("delete t from ");
            sql.Append(typeof(T).Name);
            sql.Append( " t");


            var where = BuildWhere();

            if (string.IsNullOrEmpty(where) && _model != null)
            {
                foreach (var p in _model.GetType().GetProperties())
                {
                    if (p.Name.ToLowerInvariant() == "id")
                    {
                        sql.Append($" where id = @{p.Name}");
                        break;
                    }
                }
            }
            else
            {
                sql.Append(" " + where);
            }







            //StringBuilder sql = new StringBuilder();

            //sql.Append($"insert into {_model.GetType().Name} ({string.Join(",", p1)}) values ({string.Join(",", p2)});");

            return sql;
        }
        public int Execute()
        {

            var sql = buildSql();

            var result = 1; //_connection.ExecuteScalar<int>(sql.ToString(), _model);

            return result;
        }


    }
}
