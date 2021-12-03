using System.Collections.Generic;
using Admin.Core.Common.Output;
using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.Cache
{
    /// <summary>
    /// �������
    /// </summary>
    [ProcessLog]
    public interface ICacheService
    {
        /// <summary>
        /// �����б�
        /// </summary>
        /// <returns></returns>
        List<object> List();

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        Task<bool> ClearAsync(string cacheKey);
    }
}