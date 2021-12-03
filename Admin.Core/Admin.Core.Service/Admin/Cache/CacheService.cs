using Admin.Core.Common.Cache;
using Admin.Core.Common.Output;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Service.Admin.Cache
{
    [Injection]
    public class CacheService : BaseService, ICacheService
    {
        public CacheService()
        {
        }

        public List<object> List()
        {
            var list = new List<object>();
            var cacheKey = typeof(CacheKey);
            var fields = cacheKey.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (var field in fields)
            {
                var descriptionAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

                list.Add(new
                {
                    field.Name,
                    Value = field.GetRawConstantValue().ToString(),
                    descriptionAttribute?.Description
                });
            }

            return list;
        }

        public async Task<bool> ClearAsync(string cacheKey)
        {
            Logger.LogWarning($"{User.Id}.{User.Name}清除缓存[{cacheKey}]");
            await Cache.DelByPatternAsync(cacheKey);
            return true;
        }
    }
}