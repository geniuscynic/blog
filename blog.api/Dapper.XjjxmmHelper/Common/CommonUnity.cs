using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dapper.XjjxmmHelper.Common
{
    public class CommonUnity
    {
        public static bool IsKey(IEnumerable<CustomAttributeData> customAttributeDatas)
        {
            var attribute = customAttributeDatas.FirstOrDefault(t => t.AttributeType == typeof(XjjxmmKeyAttribute));

            return attribute != null;
        }

        public static string GetFieldName(IEnumerable<CustomAttributeData> customAttributeDatas)
        {
            return CommonUnity.GetFieldName(customAttributeDatas, "FieldName");
        }

        public static string GetFieldName(IEnumerable<CustomAttributeData> customAttributeDatas, string memberName)
        {
            var attribute = customAttributeDatas.FirstOrDefault(t => t.AttributeType == typeof(XjjxmmFieldAttribute));

            if (attribute != null)
            {
                var argument = attribute.NamedArguments.FirstOrDefault(t => t.MemberName == "FieldName");

                var val = argument.TypedValue.Value?.ToString() ?? "";

                return val.Trim();
            }

            return "";

            //IList<CustomAttributeData> lstAttr = oProperty.GetCustomAttributesData();
            //foreach (var oAttr in node.Member.CustomAttributes)
            //{
            //    //得到每一个特性类的全称
            //    Console.WriteLine("特性类的名称" + oAttr.AttributeType.FullName);
            //    Console.WriteLine("特性类成员如下：");
            //    //得到特性类的所有参数
            //    var lstAttrArgu = oAttr.NamedArguments;
            //    foreach (var oAttrAru in lstAttrArgu)
            //    {
            //        //取每个特性类参数的键值对
            //        Console.WriteLine(oAttrAru.MemberName + "=" + oAttrAru.TypedValue.Value);
            //    }
            //    //Console.WriteLine(oAttr.AttributeType+"——"+oAttr.NamedArguments);
            //}
        }
    }
}
