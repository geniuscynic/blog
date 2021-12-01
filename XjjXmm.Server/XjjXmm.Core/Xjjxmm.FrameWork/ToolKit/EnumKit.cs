using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace XjjXmm.FrameWork.ToolKit
{
    public static class EnumExtensions
    {
        public static string ToDescription(this Enum item)
        {
            string name = item.ToString();
            var desc = item.GetType().GetField(name)?.GetCustomAttribute<DescriptionAttribute>();
            return desc?.Description ?? name;
        }

        public static long ToInt64(this Enum item)
        {
            return Convert.ToInt64(item);
        }

        public static int ToInt(this Enum item)
        {
            return Convert.ToInt32(item);
        }

    }

    //public static class EnumExtension
    //{
    //    public static bool IsNullOrWhiteSpace(this string s)
    //    {
    //        return string.IsNullOrWhiteSpace(s);
    //    }

    //    public static int ToInt(this Enum s)
    //    {
    //        return (int) s;
    //    }
    //}
}
