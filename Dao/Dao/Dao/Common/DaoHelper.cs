using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp1.Dao.Common
{
    public class DaoHelper
    {
        public static string GetTableName(Type type)
        {
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }

            return type.Name;
        }

        public static IEnumerable<Member> GetProperties(Type type)
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

        public static Member GetPropertyMember(PropertyInfo propertyInfo)
        {
            var customAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

            if (customAttribute != null && customAttribute.Ignore)
            {
                return null;
            }

            var member = new Member();
            member.Parameter = propertyInfo.Name;
            member.ColumnName = propertyInfo.Name;

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

        public static Type GetType(Type type)
        {
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            else if (type.IsGenericType)
            {
                return type.GetGenericArguments()[0];
            }
            else
            {
                return type;
            }
        }

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

            var tableName = DaoHelper.GetTableName(type);
            var properties = DaoHelper.GetProperties(type);

            return (tableName, properties);

        }
    }
}
