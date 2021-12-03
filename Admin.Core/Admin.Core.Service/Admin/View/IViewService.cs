using System.Collections.Generic;
using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.View.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.View.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.View
{
    /// <summary>
    /// 视图服务
    /// </summary>
    [ProcessLog]
    public interface IViewService
    {
        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ViewGetOutput> GetAsync(long id);

        Task<List<ViewListOutput>> ListAsync(string key);

        Task<PageOutput<ViewEntity>> PageAsync(PageInput<ViewEntity> model);

        Task<bool> AddAsync(ViewAddInput input);

        Task<bool> UpdateAsync(ViewUpdateInput input);

        Task<bool> DeleteAsync(long id);

        Task<bool> SoftDeleteAsync(long id);

        Task<bool> BatchSoftDeleteAsync(long[] ids);

        Task<bool> SyncAsync(ViewSyncInput input);
    }
}