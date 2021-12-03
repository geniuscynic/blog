using System.Collections.Generic;
using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.Api.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.Api.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.Api
{
    /// <summary>
    /// 接口服务
    /// </summary>
    [ProcessLog]
    public interface IApiService
    {
        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiGetOutput> GetAsync(long id);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<ApiListOutput>> ListAsync(string key);

        /// <summary>
        /// 获得分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PageOutput<ApiEntity>> PageAsync(PageInput<ApiEntity> model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AddAsync(ApiAddInput input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(ApiUpdateInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(long id);

        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> BatchSoftDeleteAsync(long[] ids);

        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SyncAsync(ApiSyncInput input);
    }
}