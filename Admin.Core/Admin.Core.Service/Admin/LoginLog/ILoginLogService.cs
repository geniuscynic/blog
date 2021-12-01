using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.LoginLog.Input;
using System.Threading.Tasks;

namespace Admin.Core.Service.Admin.LoginLog
{
    public interface ILoginLogService
    {
        Task<IResponseOutput> PageAsync(PageInput<LoginLogEntity> input);

        Task<IResponseOutput<long>> AddAsync(LoginLogAddInput input);
    }
}