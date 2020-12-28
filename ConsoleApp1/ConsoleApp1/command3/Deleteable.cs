using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.visitor3;

namespace ConsoleApp1
{
    public class Deleteable<T>
    {
        private readonly SqlConnection _connection;
        private readonly T _model;

       
        private readonly WhereExpressionVisitor _whereVisitor;

        public Deleteable(SqlConnection connection)
        {
            _connection = connection;

            _whereVisitor=new WhereExpressionVisitor();


        }


        public Deleteable(SqlConnection connection, T model)
        {
            _whereVisitor = new WhereExpressionVisitor();

            _connection = connection;
            _model = model;
        }



        public Deleteable<T> Where(Expression<Func<T, bool>> predicate)
        {
            _whereVisitor.Run(predicate);

            return this;
        }

       

        private StringBuilder buildSql()
        {
            var sql = new StringBuilder();

            if (_whereVisitor.Result.Sql.Length > 0)
            {
                sql.Append($"delete {_whereVisitor.Result.Prefix} from ");
                sql.Append(typeof(T).Name);
                sql.Append(" {_whereVisitor.Result.Prefix} ");
            }
            else
            {
                sql.Append($"delete from {typeof(T).Name}  ");
            }




            if (_whereVisitor.Result.Sql.Length==0 && _model != null)
            {
                var properties = XjjxmmExpressionVistorHelper.VisitProperty(_model.GetType().GetProperties(), "");

                var property = properties.SingleOrDefault(t => t.IsKey);
                sql.Append($" where {property.FieldName} = @{property.OriginFieldName}");

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
                sql.Append(_whereVisitor.Result.Sql);
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
