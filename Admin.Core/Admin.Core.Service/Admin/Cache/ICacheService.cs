using System.Collections.Generic;
using Admin.Core.Common.Output;
using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.Cache
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    [ProcessLog]
    public interface ICacheService
    {
        /// <summary>
        /// 缓存列表
        /// </summary>
        /// <returns></returns>
        List<object> List();

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        Task<bool> ClearAsync(string cacheKey);
    }
}