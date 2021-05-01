using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DoCare.Zkzx.Core.Database.Imp.Command.MsSql;
using DoCare.Zkzx.Core.Database.Imp.Command.MySql;
using DoCare.Zkzx.Core.Database.Imp.Command.Oracle;
using DoCare.Zkzx.Core.Database.Imp.Operate;
using DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate;
using DoCare.Zkzx.Core.Database.Imp.Operate.OracleOperate;
using DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Interface.Operate;
using DoCare.Zkzx.Core.Database.Utility;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace DoCare.Zkzx.Core.Database.Utility
{
    public class DatabaseFactory
    {

        public static IInsertable<T> CreateInsertable<T, TEntity>(DbInfo builder, TEntity model)
        {
            return builder.DbType switch
            {
                DatabaseProvider.MsSql => new SqlInsertable<T, TEntity>(builder, model),
                DatabaseProvider.MySql => new MySqlInsertable<T, TEntity>(builder, model),
                DatabaseProvider.Oracle => new OracleInsertable<T, TEntity>(builder, model),
                _ => new Insertable<T, TEntity>(builder, model)
            };
        }

        public static ISaveable<T> CreateSaveable<T, TEntity>(DbInfo builder, TEntity model)
        {
            return builder.DbType switch
            {
                DatabaseProvider.MsSql => new SqlSaveable<T, TEntity>(builder, model),
                DatabaseProvider.MySql => new MySqlSaveable<T, TEntity>(builder, model),
                DatabaseProvider.Oracle => new OracleSqlSaveable<T, TEntity>(builder, model),
                _ => new Saveable<T, TEntity>(builder, model)
            };
        }

        public static IUpdateable<T> CreateUpdateable<T>(DbInfo builder)
        {
            return builder.DbType switch
            {
                DatabaseProvider.MsSql => new SqlUpdateable<T>(builder),
                DatabaseProvider.MySql => new MySqlUpdateable<T>(builder),
                DatabaseProvider.Oracle => new OracleUpdateable<T>(builder),
                _ => new Updateable<T>(builder)
            };
        }

        public static IDoCareQueryable<T> CreateQueryable<T>(DbInfo info)
        {
            return info.DbType switch
            {
                DatabaseProvider.MsSql  => new SqlQueryable<T>(info),
                DatabaseProvider.MySql  => new MySqlQueryable<T>(info),
                _ => new OracleQueryable<T>(info),

            };
        }

        public static IComplexQueryable<T> CreateComplexQueryable<T>(DbInfo info, string alias)
        {
            var provider = new QueryableProvider(info, alias);

            return new ComplexQueryable<T>(provider);
            //return connection switch
            //{
            //    SqlConnection _ => new ComplexQueryable<T>(provider),
            //    MySqlConnection _ => new ComplexQueryable<T>(provider),
            //    OracleConnection _ => new ComplexQueryable<T>(provider),
            //    _ => new ComplexQueryable<T>(connection, alias)
            //};
        }

        //todo 不合理
        public static IReaderableCommand<T> CreateReaderableCommand<T>(DbInfo info, StringBuilder sql, Dictionary<string, object> sqlParameter)
        {
            return info.DbType switch
            {
                DatabaseProvider.MsSql  => new MsSqlReaderableCommand<T>(info, sql, sqlParameter),
                DatabaseProvider.MySql  => new MySqlReaderableCommand<T>(info, sql, sqlParameter),
                _ => new OracleReaderableCommand<T>(info, sql, sqlParameter),
            };
        }

        public static IDeleteable<T> CreateDeleteable<T>(DbInfo info)
        {
            return info.DbType switch
            {
                DatabaseProvider.MsSql  => new SqlDeleteable<T>(info),
                DatabaseProvider.MySql  => new MySqlDeleteable<T>(info),
                DatabaseProvider.Oracle  => new OracleDeleteable<T>(info),
                _ => new Deleteable<T>(info)
            };
        }


        internal static ISqlFuncVisit CreateSqlFunc(DbInfo dbInfo)
        {
            return dbInfo.DbType switch
            {
                DatabaseProvider.MsSql => new OracleSqlFunc(),
                DatabaseProvider.MySql => new OracleSqlFunc(),
                DatabaseProvider.Oracle => new OracleSqlFunc(),
                _ => new OracleSqlFunc()
            };
        }

       

    }
}
