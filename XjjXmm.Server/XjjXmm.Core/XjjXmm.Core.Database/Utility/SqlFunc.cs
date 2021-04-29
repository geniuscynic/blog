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
        private static string Visit(MethodCallExpression expression, ISqlFuncVisit visit)
        {
            var sqlFunc = visit;

            Type p = sqlFunc.GetType();

            MethodInfo m = p.GetMethod(expression.Method.Name);

            switch (m?.Name)
            {
                case "Like":

                    var p1 = ProviderHelper.VisitMember(expression.Arguments[0] as MemberExpression).Express;
                    var p2 = Expression.Lambda(expression.Arguments[1]).Compile().DynamicInvoke();  

                    return m.Invoke(sqlFunc, new []{ p1, p2 } ).ToString();
                   
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

        public static string Visit(MethodCallExpression expression, DatabaseProvider connection)
        {
            var sqlFunc = DatabaseFactory.CreateSqlFunc(connection);

            

            return Visit(expression, sqlFunc);
        }

        public static string Visit(MethodCallExpression expression, IDbConnection connection)
        {
            var sqlFunc = DatabaseFactory.CreateSqlFunc(connection);

            return Visit(expression, sqlFunc);
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
            return $"{p1} like '%{p2}%'";
        }
    }
}
