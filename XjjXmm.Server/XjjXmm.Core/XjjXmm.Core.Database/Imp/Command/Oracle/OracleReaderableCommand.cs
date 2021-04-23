using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Command.Oracle
{
    public class OracleReaderableCommand<T> : ReaderableCommand<T>
    {
        public OracleReaderableCommand(IDbConnection connection, StringBuilder sql, Dictionary<string, object> sqlParameter, Aop aop) : base(connection, sql, sqlParameter, aop)
        {
        }


        public override async Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {
            

            var countSql = $"select count(1) from ({_sql}) t";

            var total = await _connection.ExecuteScalarAsync<int>(countSql, _sqlParameter);

           
            var pageSql = $"select ROWNUM rn, t.* from ({_sql}) t where ROWNUM <= {(pageIndex) * pageSize} ";
            pageSql = $"select * from ({pageSql}) t where rn > { (pageIndex - 1) * pageSize}";

            _sql.Clear();
            _sql.Append(pageSql);

            var result = await EnumerableDelegate(async () => await _connection.QueryAsync<T>(_sql.ToString(), _sqlParameter));

            return (result, total);
            // _sql.Append($" limit {pageIndex}, {pageSize}");

           
        }

        
    }
}
