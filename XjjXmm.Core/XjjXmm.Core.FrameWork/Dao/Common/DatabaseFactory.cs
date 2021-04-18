using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using DoCare.Extension.Dao.Imp.Operate;
using DoCare.Extension.Dao.Imp.Operate.MySqlOperate;
using DoCare.Extension.Dao.Imp.Operate.OracleOperate;
using DoCare.Extension.Dao.Imp.Operate.SqlOperate;
using DoCare.Extension.Dao.Interface.Operate;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace DoCare.Extension.Dao.Common
{
    public class DatabaseFactory
    {
        public const string ParamterSplit = "@@@";

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

        public static IXXQueryable<T> CreateQueryable<T>(IDbConnection connection, Aop aop)
        {
            return connection switch
            {
                SqlConnection _ => new SqlQueryable<T>(connection) { Aop = aop },
                MySqlConnection _ => new MySqlQueryable<T>(connection) { Aop = aop },
                OracleConnection _ => new OracleQueryable<T>(connection) { Aop = aop },
                _ => new Queryable<T>(connection) { Aop = aop }
            };
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

    }
}
