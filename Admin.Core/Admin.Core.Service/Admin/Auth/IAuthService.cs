using Admin.Core.Common.Output;
using Admin.Core.Service.Admin.Auth.Input;
using System.Threading.Tasks;

namespace Admin.Core.Service.Admin.Auth
{
    /// <summary>
    /// Ȩ�޷���
    /// </summary>
    public interface IAuthService
    {
        Task<IResponseOutput> LoginAsync(AuthLoginInput input);

        Task<IResponseOutput> GetUserInfoAsync();

        Task<IResponseOutput> GetVerifyCodeAsync(string lastKey);

        Task<IResponseOutput> GetPassWordEncryptKeyAsync();
    }
}