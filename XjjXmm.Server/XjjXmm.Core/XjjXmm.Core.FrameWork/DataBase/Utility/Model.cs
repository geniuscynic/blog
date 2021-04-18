using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DoCare.Extension.DataBase.Utility
{

    public enum DatabaseProvider
    {
        Oracle,
        MsSql,
        MySql
    }


    public enum JoinType
    {
        Left,
        Inner
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

    }

    public class Member
    {
        public bool IsIdentity { get; set; } = false;
        public bool IsPrimaryKey { get; set; } = false;

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
    }

    public class ProviderModel
    {
        public int Start;
        public readonly Dictionary<string, object> Parameter;

        public readonly string DataParamterPrefix;


        public ProviderModel(string dataParamterPrefix, Dictionary<string, object> parameter, int start)
        {
            this.DataParamterPrefix = dataParamterPrefix;
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
