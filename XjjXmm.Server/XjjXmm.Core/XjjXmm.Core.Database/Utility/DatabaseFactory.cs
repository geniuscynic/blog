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
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace DoCare.Zkzx.Core.Database.Utility
{
    public class DatabaseFactory
    {
        

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


        public static IInsertable<T> CreateInsertable<T, TEntity>(IDbConnection connection, TEntity model, Aop aop)
        {
            return connection switch
            {
                SqlConnection _ => new SqlInsertable<T, TEntity>(connection, model) { Aop = aop},
                MySqlConnection _ => new MySqlInsertable<T, TEntity>(connection, model) { Aop = aop },
                OracleConnection _ => new OracleInsertable<T, TEntity>(connection, model) { Aop = aop },
                _ => new Insertable<T, TEntity>(connection, model) { Aop = aop }
            };
        }

        public static ISaveable<T> CreateSaveable<T, TEntity>(IDbConnection connection, TEntity model, Aop aop)
        {
            return connection switch
            {
                SqlConnection _ => new SqlSaveable<T, TEntity>(connection, model) { Aop = aop },
                MySqlConnection _ => new MySqlSaveable<T, TEntity>(connection, model) { Aop = aop },
                OracleConnection _ => new OracleSqlSaveable<T,TEntity>(connection, model) { Aop = aop },
                _ => new Saveable<T,TEntity>(connection, model) { Aop = aop }
            };
        }


        public static IUpdateable<T> CreateUpdateable<T>(IDbConnection connection, Aop aop)
        {
            return connection switch
            {
                SqlConnection _ => new SqlUpdateable<T>(connection) { Aop = aop },
                MySqlConnection _ => new MySqlUpdateable<T>(connection) { Aop = aop },
                OracleConnection _ => new OracleUpdateable<T>(connection) { Aop = aop },
                _ => new Updateable<T>(connection) { Aop = aop }
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

    }
}
