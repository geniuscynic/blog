using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp1
{
    public class Insertable<T>
    {
        private readonly SqlConnection _connection;
        private readonly T _model;


        public Insertable(SqlConnection connection, T model)
        {
            _connection = connection;
            _model = model;
        }

        private StringBuilder buildSql()
        {
            var p1 = new List<string>();
            var p2 = new List<string>();
            foreach (var p in _model.GetType().GetProperties())
            {
                if (p.Name.ToLowerInvariant() == "id")
                {
                            continue;
                           
                }

                p1.Add(p.Name.ToLowerInvariant());
                p2.Add($"@{p.Name}");

                //Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(_model));
            }

            StringBuilder sql = new StringBuilder();

            sql.Append($"insert into {_model.GetType().Name} ({string.Join(",", p1)}) values ({string.Join(",", p2)});");

            return sql;
        }
        public int Execute()
        {

            var sql = buildSql();

            sql.Append("select  @@IDENTITY;");


            var result = _connection.ExecuteScalar<int>(sql.ToString(), _model);

            return result;
        }


    }
}
