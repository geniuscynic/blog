using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Dao.Common;
using ConsoleApp1.Dao.Interface.Operate;
using Dapper;

namespace ConsoleApp1.Dao.Imp.Operate
{
    public class Insertable<T>  : IInsertable<T>
    {
        private readonly IDbConnection _connection;
        private readonly T _model;

        //private string sql = "insert into {0}  values ({1});";
        public Insertable(IDbConnection connection, T model)
        {
            _connection = connection;
            _model = model;
        }

        private StringBuilder BuildSql()
        {
            var columnList = new List<string>();
            var parameterList = new List<string>();

            var type = typeof(T);

            var (tableName, properties) = DaoHelper.GetMetas(type);

            foreach (var p in properties)
            {
                if (p.IsIdentity)
                {
                    continue;
                }

                columnList.Add(p.ColumnName);
                parameterList.Add($"@{p.Parameter}");

                //Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(_model));
            }

            StringBuilder sql = new StringBuilder();

            sql.Append($"insert into {tableName} ({string.Join(",", columnList)}) values ({string.Join(",", parameterList)});");

            return sql;
        }
        
        public async Task<int> Execute()
        {

            var sql = BuildSql();


           var result = await _connection.ExecuteAsync(sql.ToString(), _model);

            return result;
        }


    }
}
