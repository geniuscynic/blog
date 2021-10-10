using System.Collections.Generic;
using EmitMapper;

namespace XjjXmm.FrameWork.Mapper
{
    public static class EmitMapperExtension
    {
        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="S">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns>映射结果</returns>
        public static T MapTo<S, T>(this S source)
        {
            if (source == null) return default;

            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<S, T>();
            var result = mapper.Map(source);
            return result;
        }

        /// <summary>
        /// 对象列表映射
        /// </summary>
        /// <typeparam name="S">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns>映射结果</returns>
        public static IEnumerable<T> MapTo<S, T>(this IEnumerable<S> sources)
        {
            if (sources == null) return default;

            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<S, T>();
            return mapper.MapEnum(sources);
        }
    }
}
