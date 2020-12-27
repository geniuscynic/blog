using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.attribute
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


    public class Member
    {
        public string FieldName { get; set; }
        public string Prefix { get; set; }

        public string OriginFieldName { get; set; }

        public bool IsKey { get; set; }

        public string WhereExpression => $"{Prefix}.{FieldName}";

        public string SelectExpression => FieldName == OriginFieldName ? $"{Prefix}.{FieldName}" : $"{Prefix}.{FieldName} as {OriginFieldName}";
    }
}
