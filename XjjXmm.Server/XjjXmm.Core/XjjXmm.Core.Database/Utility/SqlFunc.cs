using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace DoCare.Zkzx.Core.Database.Utility
{
    public class SqlFunc
    {
        public static bool IsNull(string val)
        {
            return true;
        }

        public static bool Like(string p1, string p2)
        {
            return true;
        }
    }

    public class SqlFunVisit
    {
        public static string Visit(MethodCallExpression expression, IDbConnection connection)
        {
            var sqlFunc = DatabaseFactory.CreateSqlFunc(connection);

            Type p = sqlFunc.GetType();

            MethodInfo m = p.GetMethod(expression.Method.Name);

            var parms = new List<object>();
            foreach (var expression1 in expression.Arguments)
            {
                var field = ProviderHelper.VisitMember(expression1 as MemberExpression);
                //var parameterExpression = (ParameterExpression) expression1;
                parms.Add(field.Express);
            }

            if (m != null)
                return m.Invoke(sqlFunc, parms.ToArray()).ToString();

            return "";
        }
    }
    internal interface ISqlFuncVisit
    {
        string IsNull(string p1);

        string Like(string p1, string p2);
    }

    internal class OracleSqlFunc : ISqlFuncVisit
    {
        public string IsNull(string p1)
        {
            return $"nvl2({p1}， 1, 0)";
        }

        public string Like(string p1, string p2)
        {
            return $"{p1} like '{p2}'";
        }
    }
}
