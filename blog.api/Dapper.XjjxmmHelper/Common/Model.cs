using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dapper.XjjxmmHelper.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XjjxmmKeyAttribute : Attribute
    {


    }

    [AttributeUsage(AttributeTargets.Property)]
    public class XjjxmmFieldAttribute : Attribute
    {
        public string FieldName { get; set; }



    }

    public class SelectModel
    {
        public StringBuilder Sql { get; set; } = new StringBuilder();

        public string TableName { get; set; }
    }

    public class WhereModel
    {
        public int Start { get; set; }

        public StringBuilder Sql { get; set; } = new StringBuilder();

        public string Prefix { get; set; } = "";

        //public StringBuilder ResultSql { get; set; } = new StringBuilder();

        public Dictionary<string, object> Parameters = new Dictionary<string, object>();
    }

    public class Member
    {
        public string FieldName { get; set; }
        public string Prefix { get; set; }

        public string OriginFieldName { get; set; }

        public bool IsKey { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public string WhereExpression => $"{Prefix}.{FieldName}";

        public string SelectExpression => FieldName == OriginFieldName ? $"{Prefix}.{FieldName}" : $"{Prefix}.{FieldName} as {OriginFieldName}";
    }
}
