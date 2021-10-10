using System;
using System.Collections;
using System.Reflection;

namespace XjjXmm.FrameWork.ToolKit
{
    public class ObjectKit
    {
        /// <summary>
        /// 是否空值
        /// </summary>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object value, PropertyInfo property)
        {
            if (value == null) return true;

            if (value is ICollection array && array.Count == 0) return true;


            if (TypeKit.IsDateTime(property.PropertyType))
            {
                if ((DateTime) value == DateTime.MinValue)
                {
                    return true;
                }
            }


            if (string.IsNullOrEmpty(value.ToString())) return true;

            return false;
        }
    }
}
