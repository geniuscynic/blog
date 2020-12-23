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
    public class Updateable<T>
    {
        private readonly SqlConnection _connection;
        private readonly T _model;

        UpdateExpressionVisitor _visitor = new UpdateExpressionVisitor();

        public Updateable(SqlConnection connection, T model)
        {
            _connection = connection;
            _model = model;
        }


        public Updateable<T> UpdateColumns<TResult>(Expression<Func<T, TResult>> predicate)
        {

            _visitor.Visit(predicate);

            return this;
        }

        private StringBuilder buildSql()
        {
            var sql = new StringBuilder();

            sql.Append("update t set ");
            _visitor.UpdateModels.ForEach(t =>
            {
                var property = _model.GetType().GetProperties().Single(x => x.Name == t.oriFieldName);


                sql.Append($"{t.fieldName}=@{t.paramterName},");

            });

            

            sql.Remove(sql.Length - 1, 1);

            sql.Append(" from ");
            sql.Append(_model.GetType().Name);
            sql.Append($" {_visitor.UpdateModels.First().Prefix}");

            foreach (var p in _model.GetType().GetProperties())
            {
                if (p.Name.ToLowerInvariant() == "id")
                {
                    sql.Append($" where id = @{p.Name}");
                    continue;
                }
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
