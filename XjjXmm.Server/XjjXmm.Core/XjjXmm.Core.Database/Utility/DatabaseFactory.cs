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
                DatabaseProvider.MsSql => new SqlSimpleQueryable<T>(info, fullSql, sqlParameter),
                DatabaseProvider.MySql => new MySqlSimpleQueryable<T>(info,  fullSql, sqlParameter),
                _ => new OracleSimpleQueryable<T>(info, fullSql, sqlParameter),

            };
        }

        public static IDoCareQueryable<T> CreateQueryable<T>(DbInfo info)
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
