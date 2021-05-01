using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DoCare.Zkzx.Core.Database.Utility
{

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

    }

    public class Member
    {
        public bool IsIdentity { get; set; } = false;
        public bool IsPrimaryKey { get; set; } = false;

        public bool IgnoreSave { get; set; } = false;

        public string ColumnName { get; set; }

        public string Parameter { get; set; }


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
