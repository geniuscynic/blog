using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Zkzx.Core.Database.Imp.Command.MySql;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Command.MsSql
{
    public class MsSqlReaderableCommand<T> : ReaderableCommand<T>
    {
        //protected MsSqlReaderableCommand()
        //{

        //}

        internal MsSqlReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter) : base(dbInfo, sql, sqlParameter)
        {
        }


        public override async Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {
            var countSql = $"select count(1) from ({Sql}) t";

            var total = await Connection.Value.ExecuteScalarAsync<int>(countSql, SqlParameter);

            Sql.Append($" offset {(pageIndex - 1) * pageSize} rows fetch next {pageSize} rows only");

            var result = await EnumerableDelegate(async () => await Connection.Value.QueryAsync<T>(Sql.ToString(), SqlParameter));

            return (result, total);
            // _sql.Append($" limit {pageIndex}, {pageSize}");

            //_sql.Append($"select ROWNUM rn, t.* from ({_sql}) t where rn < {(pageIndex + 1) * pageSize} ");
            //_sql.Append($"select * from ({_sql}) rn >= {pageIndex * pageSize}");
        }

        
    }

    //internal class MsSqlReaderableCommandBuilder : ReaderableCommandBuilder
    //{
    //    public MsSqlReaderableCommandBuilder(DbInfo dbInfo) : base(dbInfo)
    //    {
    //    }

    //    protected override ReaderableCommand<T1> GetReaderableCommand<T1>(StringBuilder sql, Dictionary<string, object> sqlParameter)
    //    {
    //        return new MsSqlReaderableCommand<T1>(dbInfo, sql, sqlParameter);
    //    }   
    //}

    //public class MsSqlReaderableCommand : ReaderableCommand
    //{
    //    protected MsSqlReaderableCommand()
    //    {

    //    }

    //    //public MsSqlReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter) : base(dbInfo, sql, sqlParameter)
    //    //{
    //    //}


    //    public override async Task<(IEnumerable<T> data, int total)> ToPageList<T>(int pageIndex, int pageSize)
    //    {
    //        var countSql = $"select count(1) from ({Sql}) t";

    //        var total = await Connection.Value.ExecuteScalarAsync<int>(countSql, SqlParameter);

    //        Sql.Append($" offset {(pageIndex - 1) * pageSize} rows fetch next {pageSize} rows only");

    //        var result = await EnumerableDelegate(async () => await Connection.Value.QueryAsync<T>(Sql.ToString(), SqlParameter));

    //        return (result, total);
    //        // _sql.Append($" limit {pageIndex}, {pageSize}");

    //        //_sql.Append($"select ROWNUM rn, t.* from ({_sql}) t where rn < {(pageIndex + 1) * pageSize} ");
    //        //_sql.Append($"select * from ({_sql}) rn >= {pageIndex * pageSize}");
    //    }

    //    internal class MsSqlReaderableCommandBuilder : ReaderableCommandBuilder
    //    {
    //        public MsSqlReaderableCommandBuilder(DbInfo dbInfo) : base(dbInfo)
    //        {
    //        }

    //        protected override ReaderableCommand GetReaderableCommand()
    //        {
    //            return new MsSqlReaderableCommand();
    //        }
    //    }
    //}
}
