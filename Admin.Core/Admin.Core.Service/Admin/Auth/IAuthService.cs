using Admin.Core.Common.Output;
using Admin.Core.Service.Admin.Auth.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.Auth.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.Auth
{
    /// <summary>
    /// Ȩ�޷���
    /// </summary>
    [ProcessLog]
    public interface IAuthService
    {
        Task<object> GetPassWordEncryptKeyAsync();

        Task<AuthUserInfoOutput> GetUserInfoAsync();

        Task<AuthGetVerifyCodeOutput> GetVerifyCodeAsync(string lastKey);

        Task<AuthLoginOutput> LoginAsync(AuthLoginInput input);
    }
}