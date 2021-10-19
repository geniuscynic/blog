using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Command.Oracle
{
    internal class OracleReaderableCommand : InnerReaderableCommand
    {
        //private OracleReaderableCommand()
        //{

        //}

        public OracleReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter) : base(dbInfo, sql, sqlParameter)
        {
        }


        public override async Task<(IEnumerable<T> data, int total)> ToPageList<T>(int pageIndex, int pageSize)
        {


            var countSql = $"select count(1) from ({Sql}) t";

            var total = await Connection.Value.ExecuteScalarAsync<int>(countSql, SqlParameter);


            var pageSql = $"select ROWNUM rn, t.* from ({Sql}) t where ROWNUM <= {(pageIndex) * pageSize} ";
            pageSql = $"select * from ({pageSql}) t where rn > { (pageIndex - 1) * pageSize}";

            Sql.Clear();
            Sql.Append(pageSql);

            var result = await EnumerableDelegate(async () => await Connection.Value.QueryAsync<T>(Sql.ToString(), SqlParameter));

            return (result, total);
            // _sql.Append($" limit {pageIndex}, {pageSize}");


        }

        //internal class OracleReaderableCommandBuilder : ReaderableCommandBuilder
        //{
        //    public OracleReaderableCommandBuilder(DbInfo dbInfo) : base(dbInfo)
        //    {
        //    }

        //    protected override ReaderableCommand<T1> GetReaderableCommand<T1>()
        //    {
        //        return new OracleReaderableCommand<T1>();
        //    }
        //}
    }

    //public class OracleReaderableCommand : ReaderableCommand
    //{

    //    private OracleReaderableCommand()
    //    {

    //    }

    //    //public OracleReaderableCommand(DbInfo dbInfo, StringBuilder sql, Dictionary<string, object> sqlParameter) : base(dbInfo, sql, sqlParameter)
    //    //{
    //    //}


    //    public override async Task<(IEnumerable<T> data, int total)> ToPageList<T>(int pageIndex, int pageSize)
    //    {


    //        var countSql = $"select count(1) from ({Sql}) t";

    //        var total = await Connection.Value.ExecuteScalarAsync<int>(countSql, SqlParameter);


    //        var pageSql = $"select ROWNUM rn, t.* from ({Sql}) t where ROWNUM <= {(pageIndex) * pageSize} ";
    //        pageSql = $"select * from ({pageSql}) t where rn > { (pageIndex - 1) * pageSize}";

    //        Sql.Clear();
    //        Sql.Append(pageSql);

    //        var result = await EnumerableDelegate(async () => await Connection.Value.QueryAsync<T>(Sql.ToString(), SqlParameter));

    //        return (result, total);
    //        // _sql.Append($" limit {pageIndex}, {pageSize}");


    //    }

    //    internal class OracleReaderableCommandBuilder : ReaderableCommandBuilder
    //    {
    //        public OracleReaderableCommandBuilder(DbInfo dbInfo) : base(dbInfo)
    //        {
    //        }

    //        protected override ReaderableCommand GetReaderableCommand()
    //        {
    //            return new OracleReaderableCommand();
    //        }
    //    }
    //}
}
