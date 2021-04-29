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

        public static IUpdateable<T> CreateUpdateable<T>(DbInfo builder, Aop aop)
        {
            return builder.DbType switch
            {
                DatabaseProvider.MsSql => new SqlUpdateable<T>(builder),
                DatabaseProvider.MySql => new MySqlUpdateable<T>(builder),
                DatabaseProvider.Oracle => new OracleUpdateable<T>(builder),
                _ => new Updateable<T>(builder)
            };
        }

        public static IDbConnection CreateConnection(string connectionString, DatabaseProvider provider)
        {
            if (provider == DatabaseProvider.MsSql)
            {
                return new SqlConnection(connectionString);
            }
            if (provider == DatabaseProvider.MySql)
            {
                return new MySqlConnection(connectionString);
            }
            if (provider == DatabaseProvider.Oracle)
            {
                return new OracleConnection(connectionString);
            }

            return null;
        }

        public static IDbConnection CreateConnection(string connectionString, string provider)
        {
            if (provider == DatabaseProvider.MsSql.ToString())
            {
                return new SqlConnection(connectionString);
            }
            if (provider == DatabaseProvider.MySql.ToString())
            {
                return new MySqlConnection(connectionString);
            }
            if (provider == DatabaseProvider.Oracle.ToString())
            {
                return new OracleConnection(connectionString);
            }

            return null;
        }

        public static string GetStatementPrefix(IDbConnection dbConnection)
        {
            return dbConnection switch
            {
                SqlConnection _ => "@",
                _ => ":"
            };
        }

        public static DatabaseProvider GetDbType(IDbConnection dbConnection)
        {
            return dbConnection switch
            {
                SqlConnection _ => DatabaseProvider.MsSql,
                MySqlConnection _ => DatabaseProvider.MySql,
                _ => DatabaseProvider.Oracle
            };
        }


       

       


        

        public static IDoCareQueryable<T> CreateQueryable<T>(IDbConnection connection, Aop aop)
        {
            return connection switch
            {
                SqlConnection _ => new SqlQueryable<T>(connection) { Aop = aop },
                MySqlConnection _ => new MySqlQueryable<T>(connection) { Aop = aop },
                _ => new OracleQueryable<T>(connection) { Aop = aop },
               
            };
        }

        public static IComplexQueryable<T> CreateComplexQueryable<T>(IDbConnection connection, Aop aop, string alias)
        {
            var provider = new QueryableProvider(connection, alias)
            {
                Aop = aop
            };

            return new ComplexQueryable<T>(provider);
            //return connection switch
            //{
            //    SqlConnection _ => new ComplexQueryable<T>(provider),
            //    MySqlConnection _ => new ComplexQueryable<T>(provider),
            //    OracleConnection _ => new ComplexQueryable<T>(provider),
            //    _ => new ComplexQueryable<T>(connection, alias)
            //};
        }

        public static IDeleteable<T> CreateDeleteable<T>(IDbConnection connection, Aop aop)
        {
            return connection switch
            {
                SqlConnection _ => new SqlDeleteable<T>(connection) { Aop = aop },
                MySqlConnection _ => new MySqlDeleteable<T>(connection) { Aop = aop },
                OracleConnection _ => new OracleDeleteable<T>(connection) { Aop = aop },
                _ => new Deleteable<T>(connection) { Aop = aop }
            };
        }

        public static IReaderableCommand<T> CreateReaderableCommand<T>(IDbConnection connection, StringBuilder sql, Dictionary<string, object> sqlParameter, Aop aop)
        {
            return connection switch
            {
                SqlConnection _ => new MsSqlReaderableCommand<T>(connection, sql, sqlParameter, aop),
                MySqlConnection _ => new MySqlReaderableCommand<T>(connection, sql, sqlParameter, aop),
                _ => new OracleReaderableCommand<T>(connection, sql, sqlParameter, aop),
            };
        }

        internal static ISqlFuncVisit CreateSqlFunc(IDbConnection dbConnection)
        {
            return dbConnection switch
            {
                SqlConnection _ => new OracleSqlFunc(),
                _ => new OracleSqlFunc()
            };
        }

        internal static ISqlFuncVisit CreateSqlFunc(DatabaseProvider dbConnection)
        {
            return dbConnection switch
            {
                DatabaseProvider.Oracle => new OracleSqlFunc(),
                _ => new OracleSqlFunc()
            };

            //return dbConnection switch
            //{
            //    DatabaseProvider.Oracle _ => new OracleSqlFunc(),
            //    _ => new OracleSqlFunc()
            //};
        }

    }
}
