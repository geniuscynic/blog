using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DoCare.Extension.DataBase.Utility
{
    static class ProviderHelper
    {
        public static (string tableName, IEnumerable<Member> members) GetMetas(Type type)
        {
            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            var tableName = ProviderHelper.GetTableName(type);
            var properties = ProviderHelper.GetProperties(type);

            return (tableName, properties);

        }

        static string GetTableName(Type type)
        {
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }

            return type.Name;
        }

        static IEnumerable<Member> GetProperties(Type type)
        {
            foreach (var propertyInfo in type.GetProperties())
            {
                var member = GetPropertyMember(propertyInfo);
                       
                if (member == null)
                {
                    continue;
                }

                yield return member;
            }
        }

        static Member GetPropertyMember(PropertyInfo propertyInfo)
        {
            var customAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

            if (customAttribute != null && customAttribute.Ignore)
            {
                return null;
            }

            var member = new Member
            {
                Parameter = propertyInfo.Name,
                ColumnName = propertyInfo.Name, 
                PropertyInfo = propertyInfo
            };

            if (customAttribute != null)
            {
                if (!string.IsNullOrEmpty(customAttribute.ColumnName))
                {
                    member.ColumnName = customAttribute.ColumnName;
                }

                member.IsIdentity = customAttribute.IsIdentity;
                member.IsPrimaryKey = customAttribute.IsPrimaryKey;
            }

            return member;
        }

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
