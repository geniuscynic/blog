using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper.XjjxmmHelper.Visitor;


namespace Dapper.XjjxmmHelper.Command
{
    public class Insertable<T>
    {
        private readonly IDbConnection _connection;
        private readonly T _model;


        public Insertable(IDbConnection connection, T model)
        {
            _connection = connection;
            _model = model;
        }

        private StringBuilder BuildSql()
        {
            var p1 = new List<string>();
            var p2 = new List<string>();

            var properties = XjjxmmExpressionVistorHelper.VisitProperty(_model.GetType().GetProperties(), "");
            foreach (var p in properties)
            {
                if (p.IsKey)
                {
                    continue;
                }

                p1.Add(p.FieldName);
                p2.Add($"@{p.OriginFieldName}");

                //Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(_model));
            }

            StringBuilder sql = new StringBuilder();

            sql.Append($"insert into {_model.GetType().Name} ({string.Join(",", p1)}) values ({string.Join(",", p2)});");

            sql.Append("select  @@IDENTITY;");

            return sql;
        }


        public int Execute()
        {

            var sql = BuildSql();

            //sql.Append("select  @@IDENTITY;");

            var result = _connection.ExecuteScalar<int>(sql.ToString(), _model);

            return result;
        }

        public async Task<int> ExecuteAsync()
        {
            var sql = BuildSql();
            //sql.Append("select  @@IDENTITY;");

            return await _connection.ExecuteScalarAsync<int>(sql.ToString(), _model);
        }


    }
}
