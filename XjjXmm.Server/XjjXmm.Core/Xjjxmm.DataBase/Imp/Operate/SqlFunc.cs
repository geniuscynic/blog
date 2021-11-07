﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.Utility;
using XjjXmm.FrameWork.ToolKit;

namespace XjjXmm.DataBase.Imp.Operate
{
    public class SqlFunc
    {
        /// <summary>
        /// 判断是否为null 相当于 nvl2({p1}， 1, 0)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string IsNull(string val)
        {
            return "1";
        }

        public static string IsNull(DateTime? val)
        {
            return "1";
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool Like(string p1, string p2)
        {
            return true;
        }

        /// <summary>
        /// 相当于 in
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool Contain(IEnumerable<string> p1, string p2)
        {
            return true;
        }

        /// <summary>
        /// 相当于 in
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool Contain(IEnumerable<long> p1, long p2)
        {
            return true;
        }

        /// <summary>
        /// 数据库日期格式话
        /// </summary>
        /// <param name="date">一般用 sysdate 之类的系统函数</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime FormatDate(string date, string format)
        {
            return DateTime.Now;

        }

        /// <summary>
        /// 数据库日期格式话
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime FormatDate(DateTime? date, string format)
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 数据库日期格式话成string
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string CovertDateToString(DateTime? date, string format)
        {
            return "";
        }


        public static string Lower(string p1)
        {
            return "";
        }

        public static string Upper(string p1)
        {
            return "";
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
        //public abstract string IsNull(DateTime? p1);

        public string Like(string p1, string p2)
        {
            return $"{p1} like '%{p2}%'";
        }

        public string Contain(IEnumerable<string> p1, string p2)
        {
            //var sb = new StringBuilder();
            //sb.Append($"{p2} in (");

            //foreach (var l in p1.ToList())
            //{
            //    sb.Append($"'{l}',");
            //}

            //sb.Remove(sb.Length - 1, 1);

            //sb.Append(")");

            //return sb.ToString();

            //p1.Select(t => a.ToString());
            return $"{p2} in ({p1.Concat(closure: "'")})";
        }

        public string Contain(IEnumerable<long> p1, long p2)
        {
            var sb = new StringBuilder();
            sb.Append($"{p2} in (");

            foreach (var l in p1.ToList())
            {
                sb.Append($"{l},");
            }

            sb.Remove(sb.Length - 1, 1);

            sb.Append(")");

            return sb.ToString();
        }

        public string FormatDate(string date, string format)
        {
            return $"to_date(to_char({date}, '{format}'), '{format}')";

        }

        public string CovertDateToString(string date, string format)
        {
            return $"to_char({date}, '{format}')";
        }

        public string Lower(string p1)
        {
            return $"LOWER({p1})";
        }

        public string Upper(string p1)
        {
            return $"UPPER({p1})";
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
