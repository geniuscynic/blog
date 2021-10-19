using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using XjjXmm.DataBase.Imp.Command.MsSql;
using XjjXmm.DataBase.Imp.Command.MySql;
using XjjXmm.DataBase.Imp.Command.Oracle;
using XjjXmm.DataBase.Imp.Operate;
using XjjXmm.DataBase.Imp.Operate.MySqlOperate;
using XjjXmm.DataBase.Imp.Operate.OracleOperate;
using XjjXmm.DataBase.Imp.Operate.SqlOperate;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.Utility;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace XjjXmm.DataBase.Utility
{
    public class DatabaseFactory
    {

        public static IInsertable<T> CreateInsertable<T, TEntity>(DbInfo builder, TEntity model)
        {
            return builder.DbType switch
            {
                DatabaseProvider.MsSql => new MsSqlInsertable<T, TEntity>(builder, model),
                DatabaseProvider.MySql => new MySqlInsertable<T, TEntity>(builder, model),
                DatabaseProvider.Oracle => new OracleInsertable<T, TEntity>(builder, model),
                _ => new OracleInsertable<T, TEntity>(builder, model)
            };
        }

        public static ISaveable<T> CreateSaveable<T, TEntity>(DbInfo builder, TEntity model)
        {
            return builder.DbType switch
            {
                DatabaseProvider.MsSql => new SqlSaveable<T, TEntity>(builder, model),
                DatabaseProvider.MySql => new MySqlSaveable<T, TEntity>(builder, model),
                DatabaseProvider.Oracle => new OracleSqlSaveable<T, TEntity>(builder, model),
                _ => new OracleSqlSaveable<T, TEntity>(builder, model)
            };
        }

        public static IUpdateable<T> CreateUpdateable<T>(DbInfo builder)
        {
            return builder.DbType switch
            {
                DatabaseProvider.MsSql => new MsSqlUpdateable<T>(builder),
                DatabaseProvider.MySql => new MySqlUpdateable<T>(builder),
                _ => new OracleUpdateable<T>(builder),

            };
        }

        public static IReaderableCommand<T> CreateSimpleQueryable<T>(DbInfo info, string fullSql, Dictionary<string, object> sqlParameter)
        {
            return info.DbType switch
            {
                DatabaseProvider.MsSql => new MsSqlSimpleQueryable<T>(info, fullSql, sqlParameter),
                DatabaseProvider.MySql => new MySqlSimpleQueryable<T>(info,  fullSql, sqlParameter),
                _ => new OracleSimpleQueryable<T>(info, fullSql, sqlParameter),

            };
        }

        public static IXjjXmmQueryable<T> CreateQueryable<T>(DbInfo info)
        {
            return info.DbType switch
            {
                DatabaseProvider.MsSql  => new MsSqlQueryable<T>(info),
                DatabaseProvider.MySql  => new MySqlQueryable<T>(info),
                _ => new OracleQueryable<T>(info),

            };
        }

        public static IComplexQueryable<T> CreateComplexQueryable<T>(DbInfo info, string alias)
        {
            // var provider = new QueryableProvider(info, alias);

            IQueryableProvider provider = info.DbType switch
            {
                DatabaseProvider.MsSql => new MsSqlQueryableProvider(info, alias),
                DatabaseProvider.MySql => new MySqlQueryableProvider(info, alias),
                _ => new OracleQueryableProvider(info, alias),

            };

            return new ComplexQueryable<T>(provider);
           
        }

       

        public static IDeleteable<T> CreateDeleteable<T>(DbInfo info)
        {
            return info.DbType switch
            {
                DatabaseProvider.MsSql  => new MsSqlDeleteable<T>(info),
                DatabaseProvider.MySql  => new MySqlDeleteable<T>(info),
                _  => new OracleDeleteable<T>(info),
               
            };
        }


        

    }
}
