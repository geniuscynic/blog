using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;
using Admin.Service.View.Input;
using Admin.Service.View.Output;
using XjjXmm.FrameWork.Common;
using Admin.Repository.View;

namespace Admin.Service.View
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
        Task<ViewGetOutput> Get(long id);

        Task<List<ViewListOutput>> List(string key);

        Task<PageOutput<ViewEntity>> Page(PageInput<ViewPageInput> model);

        Task<bool> Add(ViewAddInput input);

        Task<bool> Update(ViewUpdateInput input);

        Task<bool> Delete(long id);

        Task<bool> SoftDelete(long id);

        Task<bool> BatchSoftDelete(long[] ids);

        Task<bool> Sync(ViewSyncInput input);
    }
}