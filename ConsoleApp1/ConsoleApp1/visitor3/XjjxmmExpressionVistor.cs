using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.attribute;

namespace ConsoleApp1.visitor3
{
    public  static class XjjxmmExpressionVistorHelper
    {
        public static Member VisitMember(MemberExpression node)
        {

            var prefix = (node.Expression as ParameterExpression)?.Name ?? "";
            //var field = node.Member.Name;

            var field = CommonUnity.GetFieldName(node.Member.CustomAttributes);

            if (string.IsNullOrWhiteSpace(field))
            {
                field = node.Member.Name;
            }


            return new Member
            {
                FieldName = field,
                Prefix = prefix,
                OriginFieldName = node.Member.Name
            };
        }


        public static IEnumerable<Member> VisitProperty(PropertyInfo[] properties, string prefix)
        {
            foreach (var propertyInfo in properties)
            {
                var field = CommonUnity.GetFieldName(propertyInfo.CustomAttributes);

                if (string.IsNullOrWhiteSpace(field))
                {
                    field = propertyInfo.Name;

                }

                var isKey = CommonUnity.IsKey(propertyInfo.CustomAttributes);


                yield return new Member
                {
                    FieldName = field,
                    Prefix = prefix,
                    OriginFieldName = propertyInfo.Name,
                    IsKey = isKey ,
                    PropertyInfo = propertyInfo
                };
            }
        }


        
    }
}
