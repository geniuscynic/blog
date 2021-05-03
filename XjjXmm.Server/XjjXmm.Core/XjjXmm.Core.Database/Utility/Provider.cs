using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using DoCare.Zkzx.Core.Database.Interface.Operate;

namespace DoCare.Zkzx.Core.Database.Utility
{
    internal static class ProviderHelper
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
                member.IgnoreSave = customAttribute.IgnoreSave;
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


        public static Field VisitMember(MemberAssignment node)
        {
            string field = "";
            string prefix = "";
            MethodCallExpression expression = null;

            switch (node.Expression)
            {
                case MemberExpression nodeExpression:
                {
                    MemberExpression nodeMemberExpression = nodeExpression;


                    prefix = (nodeMemberExpression.Expression as ParameterExpression)?.Name ?? "";
                    //var field = node.Member.Name;

                    field = GetFieldName(nodeMemberExpression.Member.CustomAttributes);
                    break;
                }
                case ConstantExpression nodeExpression:
                {
                    var exp = nodeExpression;
                    var value = exp.Value;
                    field = value.ToString();
                    break;
                }
                case MethodCallExpression nodeExpression:
                    expression = nodeExpression;
                    //var value = exp.Value;
                    //field = value.ToString();
                    break;
                default:
                {
                    var value = Expression.Lambda(node.Expression).Compile().DynamicInvoke();
                    field = value.ToString();
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(field))
            {
                field = node.Member.Name;
            }




            return new Field
            {
                ColumnName = field,
                Prefix = prefix,
                Parameter = node.Member.Name,
                Expression = expression
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


        public static string VisitSqlFuc(MethodCallExpression expression, ISqlFuncVisit visit)
        {
            var sqlFunc = visit;

            Type p = sqlFunc.GetType();

            MethodInfo m = p.GetMethod(expression.Method.Name);

            switch (m?.Name)
            {
                case "Like":

                    var p1 = ProviderHelper.VisitMember(expression.Arguments[0] as MemberExpression).Express;
                    var p2 = Expression.Lambda(expression.Arguments[1]).Compile().DynamicInvoke();

                    return m.Invoke(sqlFunc, new[] { p1, p2 }).ToString();

                case "IsNull":
                    var parms = new List<object>();
                    foreach (var expression1 in expression.Arguments)
                    {
                        var field = ProviderHelper.VisitMember(expression1 as MemberExpression);
                        //var parameterExpression = (ParameterExpression) expression1;
                        parms.Add(field.Express);
                    }

                    return m.Invoke(sqlFunc, parms.ToArray()).ToString();

            }


            return "";
        }
    }
}
