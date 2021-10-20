using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xjjxmm.DataBase.Utility
{
    //internal static class DynamicMapping
    //{
    //    static void Mapping(Type TFirst, Type TSecond)
    //    {
    //        var first = Expression.Parameter(TFirst, "first");
    //        var second = Expression.Parameter(TSecond, "second");

    //        var secondSetExpression = MappingCache.MappingCache.GetSetExpression(second, first);

    //        var blockExpression = Expression.Block(first, second, secondSetExpression, first);




    //        Map = Expression.Lambda<Func<TFirst, TSecond, TFirst>>(blockExpression, first, second).Compile();
    //        var res =Expression.Lambda(blockExpression, first, second);
    //    }

    //    internal static Func<TFirst, TSecond, TFirst> Map { get; private set; }
    //}
}
