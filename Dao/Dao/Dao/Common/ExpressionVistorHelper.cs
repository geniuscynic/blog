using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleApp1.Dao.Common
{
    public  static class ExpressionVistorHelper
    {
        public static Field VisitMember(MemberExpression node)
        {

            var prefix = (node.Expression as ParameterExpression)?.Name ?? "";
            //var field = node.Member.Name;

            var field = GetFieldName(node.Member.CustomAttributes);

            if (string.IsNullOrWhiteSpace(field))
            {
                field = node.Member.Name;
            }


            return new Field
            {
                ColumnName = field,
                Prefix = prefix,
                Parameter = node.Member.Name
            };
        }

        public static Field VisitMember(MemberInfo member)
        {
            var field = GetFieldName(member.CustomAttributes);

            if (string.IsNullOrWhiteSpace(field))
            {
                field = member.Name;
            }


            return new Field
            {
                ColumnName = field,
                Parameter = member.Name
            };
        }


        private static string GetFieldName(IEnumerable<CustomAttributeData> customAttributeDatas)
        {
            var attribute = customAttributeDatas.FirstOrDefault(t => t.AttributeType == typeof(ColumnAttribute));

            if (attribute != null)
            {
                var argument = attribute.NamedArguments.FirstOrDefault(t => t.MemberName == "ColumnName");

                var val = argument.TypedValue.Value?.ToString() ?? "";

                return val.Trim();
            }

            return "";

            
        }



    }
}
