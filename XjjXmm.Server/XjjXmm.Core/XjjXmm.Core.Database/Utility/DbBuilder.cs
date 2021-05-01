using System;
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

                    StatementPrefix = ":";
                    break;
                case DatabaseProvider.MySql:
                    Connection = new Lazy<IDbConnection>(() => new MySqlConnection(connectionString));
                    DbType = provider;

                    StatementPrefix = "@";
                    break;
                case DatabaseProvider.Oracle:
                    Connection = new Lazy<IDbConnection>(() => new OracleConnection(connectionString));
                    DbType = provider;

                    StatementPrefix = "@";
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
