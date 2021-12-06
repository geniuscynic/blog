using System.Threading.Tasks;
using Admin.Service.Auth.Input;
using Admin.Service.Auth.Output;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Service.Auth
{
    [Injection]
    public class AuthService : IAuthService
    {
       

        public AuthService(
          
        )
        {
           
        }

       
        public async Task<AuthLoginOutput> LoginAsync(AuthLoginInput input)
        {
           

            return null;
        }
    }
}