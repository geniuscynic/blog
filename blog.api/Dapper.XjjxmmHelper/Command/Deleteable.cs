using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper.XjjxmmHelper.Visitor;

namespace Dapper.XjjxmmHelper.Command
{
    public class Deleteable<T>
    {
        private readonly IDbConnection _connection;
        private readonly T _model;

       
        private readonly WhereExpressionVisitor _whereVisitor;

        public Deleteable(IDbConnection connection)
        {
            _connection = connection;

            _whereVisitor=new WhereExpressionVisitor();


        }


        public Deleteable(IDbConnection connection, T model)
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

            var result = _connection.Execute(sql.ToString(), _model);

            return result;
        }


        public async Task<int> ExecuteAsync()
        {

            var sql = buildSql();

            var result = await _connection.ExecuteAsync(sql.ToString(), _model);

            return result;
        }


    }
}
