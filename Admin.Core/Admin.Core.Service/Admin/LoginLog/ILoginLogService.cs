using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.LoginLog.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.LoginLog.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.LoginLog
{
    [ProcessLog]
    public interface ILoginLogService
    {
        Task<PageOutput<LoginLogListOutput>> PageAsync(PageInput<LoginLogEntity> input);

        Task<long> AddAsync(LoginLogAddInput input);
    }
}