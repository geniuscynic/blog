using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper;

namespace DoCare.Extension.Tool
{
    public static class EmitMapperExtension
    {
        public static T MapTo<S, T>(this S source)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<S, T>();

            var result = mapper.Map(source);

            return result;
        }


        public static List<T> MapTo<S, T>(this IEnumerable<S> source)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<S, T>();

            var result = mapper.MapEnum(source).ToList();

            return result;
        }
    }
}
