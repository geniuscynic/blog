using System;
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
    public class DbInfo
    {
        internal Aop Aop { get; }
        internal Lazy<IDbConnection> Connection { get; }
        internal DatabaseProvider DbType { get; }

        internal string StatementPrefix { get; }


        internal DbInfo(string connectionString, DatabaseProvider provider,  Aop aop =null)
        {
           
            Aop = aop;

            switch (provider)
            {
                case DatabaseProvider.MsSql:
                    Connection = new Lazy<IDbConnection>(()=> new SqlConnection(connectionString));

                    DbType = provider;

                    StatementPrefix = "@";
                    break;
                case DatabaseProvider.MySql:
                    Connection = new Lazy<IDbConnection>(() => new MySqlConnection(connectionString));
                    DbType = provider;

                    StatementPrefix = ":";
                    break;
                case DatabaseProvider.Oracle:
                    Connection = new Lazy<IDbConnection>(() => new OracleConnection(connectionString));
                    DbType = provider;

                    StatementPrefix = ":";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
            }
        }

        internal DbInfo(string connectionString, string provider, Aop aop = null) : this(connectionString,
            provider.ToDatabaseProvider(), aop)
        {

        }


    }
}
