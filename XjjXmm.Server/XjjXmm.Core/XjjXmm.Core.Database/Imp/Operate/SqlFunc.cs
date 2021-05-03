using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using DoCare.Zkzx.Core.Database.Interface.Operate;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate
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

    internal abstract class SqlFuncVisit : ISqlFuncVisit
    {
        //public string IsNull(string p1)
        //{
        //    return $"nvl2({p1}， 1, 0)";
        //}

        //public string Like(string p1, string p2)
        //{
        //    return $"{p1} like '%{p2}%'";
        //}
        public abstract string IsNull(string p1);

        public string Like(string p1, string p2)
        {
            return $"{p1} like '%{p2}%'";
        }
    }

    //internal class SqlFunProvider
    //{
    //    public static string Visit(MethodCallExpression expression, ISqlFuncVisit visit)
    //    {
    //        var sqlFunc = visit;

    //        Type p = sqlFunc.GetType();

    //        MethodInfo m = p.GetMethod(expression.Method.Name);

    //        switch (m?.Name)
    //        {
    //            case "Like":

    //                var p1 = ProviderHelper.VisitMember(expression.Arguments[0] as MemberExpression).Express;
    //                var p2 = Expression.Lambda(expression.Arguments[1]).Compile().DynamicInvoke();

    //                return m.Invoke(sqlFunc, new[] { p1, p2 }).ToString();

    //            case "IsNull":
    //                var parms = new List<object>();
    //                foreach (var expression1 in expression.Arguments)
    //                {
    //                    var field = ProviderHelper.VisitMember(expression1 as MemberExpression);
    //                    //var parameterExpression = (ParameterExpression) expression1;
    //                    parms.Add(field.Express);
    //                }

    //                return m.Invoke(sqlFunc, parms.ToArray()).ToString();

    //        }


    //        return "";
    //    }


    //}

}
