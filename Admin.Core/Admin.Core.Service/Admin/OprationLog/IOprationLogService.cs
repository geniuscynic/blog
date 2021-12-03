using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.OprationLog.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.OprationLog.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.OprationLog
{
    [ProcessLog]
    public interface IOprationLogService
    {
        Task<PageOutput<OprationLogListOutput>> PageAsync(PageInput<OprationLogEntity> input);

        Task<bool> AddAsync(OprationLogAddInput input);
    }
}