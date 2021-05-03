using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Command.MySql
{
    public class MySqlReaderableCommand<T> : ReaderableCommand<T>
    {
        //private MySqlReaderableCommand()
        //{

        //}
        public MySqlReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter) : base(dbInfo, sql, sqlParameter)
        {
        }


        public override async Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {
            var countSql = $"select count(1) from ({Sql}) t";

            var total = await Connection.Value.ExecuteScalarAsync<int>(countSql, SqlParameter);

            // _sql.Append($" offset {pageIndex} rows fetch next {pageSize} rows only");
            Sql.Append($" limit { (pageIndex - 1) * pageSize}, {pageSize}");

            var result = await EnumerableDelegate(async () => await Connection.Value.QueryAsync<T>(Sql.ToString(), SqlParameter));

            return (result, total);
            // _sql.Append($" limit {pageIndex}, {pageSize}");

            //_sql.Append($"select ROWNUM rn, t.* from ({_sql}) t where rn < {(pageIndex + 1) * pageSize} ");
            //_sql.Append($"select * from ({_sql}) rn >= {pageIndex * pageSize}");
        }

        //internal class MySqlReaderableCommandBuilder : ReaderableCommandBuilder
        //{
        //    public MySqlReaderableCommandBuilder(DbInfo dbInfo) : base(dbInfo)
        //    {
        //    }

        //    protected override ReaderableCommand<T1> GetReaderableCommand<T1>()
        //    {
        //        return new MySqlReaderableCommand<T1>();
        //    }
        //}
    }

    //public class MySqlReaderableCommand : ReaderableCommand
    //{
    //    private MySqlReaderableCommand()
    //    {

    //    }
    //    //public MySqlReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter) : base(dbInfo, sql, sqlParameter)
    //    //{
    //    //}


    //    public override async Task<(IEnumerable<T> data, int total)> ToPageList<T>(int pageIndex, int pageSize)
    //    {
    //        var countSql = $"select count(1) from ({Sql}) t";

    //        var total = await Connection.Value.ExecuteScalarAsync<int>(countSql, SqlParameter);

    //        // _sql.Append($" offset {pageIndex} rows fetch next {pageSize} rows only");
    //        Sql.Append($" limit { (pageIndex - 1) * pageSize}, {pageSize}");

    //        var result = await EnumerableDelegate(async () => await Connection.Value.QueryAsync<T>(Sql.ToString(), SqlParameter));

    //        return (result, total);
    //        // _sql.Append($" limit {pageIndex}, {pageSize}");

    //        //_sql.Append($"select ROWNUM rn, t.* from ({_sql}) t where rn < {(pageIndex + 1) * pageSize} ");
    //        //_sql.Append($"select * from ({_sql}) rn >= {pageIndex * pageSize}");
    //    }

    //    internal class MySqlReaderableCommandBuilder : ReaderableCommandBuilder
    //    {
    //        public MySqlReaderableCommandBuilder(DbInfo dbInfo) : base(dbInfo)
    //        {
    //        }

    //        protected override ReaderableCommand GetReaderableCommand()
    //        {
    //            return new MySqlReaderableCommand();
    //        }
    //    }
    //}
}
