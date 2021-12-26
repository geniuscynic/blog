using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Service.Auth
{
    /// <summary>
    /// Ȩ�޷���
    /// </summary>
    [ProcessLog]
    public interface IAuthService
    {

        Task<object> GetPassWordEncryptKey();

        Task<AuthLoginOutput> Login(AuthLoginInput input);

        Task<AuthUserInfoOutput> GetUserInfo();
    }
}