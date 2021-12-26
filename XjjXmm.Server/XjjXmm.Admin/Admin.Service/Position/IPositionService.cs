using Admin.Repository.Position;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Service.Position
{
    /// <summary>
    /// ¸ÚÎ»·þÎñ
    /// </summary>
    [ProcessLog]
    public interface IPositionService
    {
        Task<PositionGetOutput> Get(long id);
        Task<PageOutput<PositionListOutput>> Page(PageInput<PositionListInput> input);
        Task<bool> Add(PositionAddInput input);
        Task<bool> Update(PositionUpdateInput input);
        Task<bool> Delete(long id);
        Task<bool> SoftDelete(long id);
        Task<bool> BatchSoftDelete(long[] ids);
    }
}