using System.Threading.Tasks;
using Admin.Service.Auth.Input;
using Admin.Service.Auth.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Service.Auth
{
    /// <summary>
    /// 权限服务
    /// </summary>
    [ProcessLog]
    public interface IAuthService
    {

        Task<object> GetPassWordEncryptKey();

        Task<AuthLoginOutput> Login(AuthLoginInput input);
    }
}