using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace XjjXmm.DataBase.Utility
{

    internal class OracleClobParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly string value;

        public OracleClobParameter(string value)
        {
            this.value = value;
        }

        public void AddParameter(IDbCommand command, string name)
        {


            // accesing the connection in open state.
            //var clob = new OracleClob(command.Connection as OracleConnection);

            //// It should be Unicode oracle throws an exception when
            //// the length is not even.
            //var bytes = System.Text.Encoding.Unicode.GetBytes(value);
            //var length = System.Text.Encoding.Unicode.GetByteCount(value);

            //int pos = 0;
            //int chunkSize = 1024; // Oracle does not allow large chunks.

            //while (pos < length)
            //{
            //    chunkSize = chunkSize > (length - pos) ? chunkSize = length - pos : chunkSize;
            //    clob.Write(bytes, pos, chunkSize);
            //    pos += chunkSize;
            //}

            //var param = new OracleParameter(name, OracleDbType.Clob);
            //param.Value = clob;

            //command.Parameters.Add(param);


            command.Parameters.Add(new OracleParameter(name, OracleDbType.Clob, value, ParameterDirection.Input));
        }
    }

    //    sealed class BigString : Dapper.SqlMapper.ICustomQueryParameter
    //    {
    //        public void AddParameter(IDbCommand command, string name)
    //        {

    //            bool add = !command.Parameters.Contains(name);
    //            IDbDataParameter param;
    //            if (add)
    //            {
    //                param = command.CreateParameter();
    //                param.ParameterName = name;
    //            }
    //            else
    //            {
    //                param = (IDbDataParameter)command.Parameters[name];
    //            }

    //            param.Value = SqlMapper.SanitizeParameterValue(Value);
    //#
    //            if (Length == -1 && Value != null && Value.Length <= DefaultLength)
    //            {
    //                param.Size = DefaultLength;
    //            }
    //            else
    //            {
    //                param.Size = Length;
    //            }

    //            param.DbType = OracleDbType.Clob; IsAnsi ? (IsFixedLength ? DbType.AnsiStringFixedLength : DbType.AnsiString) : (IsFixedLength ? DbType.StringFixedLength : DbType.String);
    //            if (add)
    //            {
    //                command.Parameters.Add(param);
    //            }

    //            //OracleClob
    //        }
    //    }

    public enum DatabaseProvider
    {
        Oracle,
        MsSql,
        MySql
    }

    public static class DatabaseProviderExtension
    {
        public static DatabaseProvider ToDatabaseProvider(this string provider)
        {
            if (provider == DatabaseProvider.MsSql.ToString())
            {
                return DatabaseProvider.MsSql;
            }
            else if (provider == DatabaseProvider.MySql.ToString())
            {
                return DatabaseProvider.MySql;
            }
            else if (provider == DatabaseProvider.Oracle.ToString())
            {
                return DatabaseProvider.Oracle;
            }

            throw new  Exception($"无效的provider：${provider}");
        }
    }


    public enum JoinType
    {
        Left,
        Inner
    }

    public enum OrderByType
    {
        ASC,
        DESC
    }

    public class JoinInfo
    {
        //public JoinInfo(JoinType joinType, Expression<Func<>>)
    }
    public class JoinInfos
    {
       // public JoinInfos(JoinType joinType, )
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string name)
        {
            this.Name = name;

        }
        public string Name  { get; set; }

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public bool IsIdentity { get; set; } = false;

        public bool IsPrimaryKey { get; set; } = false;

        public string ColumnName { get; set; }

        public bool Ignore { get; set; }

        public bool IgnoreSave { get; set; }

        public bool IsBigText { get; set; }
    }

    public class Member
    {
        public bool IsIdentity { get; set; } = false;
        public bool IsPrimaryKey { get; set; } = false;

        public bool IgnoreSave { get; set; } = false;

        public string ColumnName { get; set; }

        public string Parameter { get; set; }

        public bool IsBigText { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

    }

    public class Field
    {
        public string ColumnName { get; set; }

        public string Parameter { get; set; }

        public string Prefix { get; set; }

        public string Express => !string.IsNullOrEmpty(Prefix) ? $"{Prefix}.{ColumnName}" : ColumnName;

        public MethodCallExpression Expression { get; set; }
    }

    public class ProviderModel
    {
        internal DbInfo DbInfo { get; }
        internal int Start { get; set; }
        internal Dictionary<string, object> Parameter { get; }



        internal ProviderModel(DbInfo dbClientParamter, Dictionary<string, object> parameter, int start)
        {
            DbInfo = dbClientParamter;
            this.Start = start;
            this.Parameter = parameter;
          
        }
    }

    public class WhereModel
    {
       

        public StringBuilder Sql { get; set; } = new StringBuilder();



        public string Prefix { get; set; } = "";

    }
}
