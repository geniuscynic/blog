using System.Threading.Tasks;
using Permission.Entity;
using Permission.IService;
using Permission.Model;
using Permission.Repository;
using XjjXmm.Core.FrameWork.Common;
using XjjXmm.Core.FrameWork.Mapper;
using XjjXmm.Core.FrameWork.ToolKit;
using XjjXmm.Core.ToolKit;

namespace Permission.Service
{
    public class AccountService : IAccountService {

        public UserRepository UserRepository { get; set; }

        public Task<BussinessModel<bool>> AddUser(UserModel userModel)
        {
            if (!userModel.Id.IsNullOrWhiteSpace())
            {
                 UserRepository.
            }
        }

        public Task<BussinessModel<bool>> EditUser(UserModel userModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BussinessModel<UserModel>> FindUserByLoginPassword(LoginModel loginModel)
        {
            var user = await UserRepository.FirstOrDefault(t => t.Account == loginModel.Login && t.Password == loginModel.Password);

            if (user == null)
            {
                return new BussinessModel<UserModel>(null);
            }

            return new BussinessModel<UserModel>(user.MapTo<User, UserModel>());
        }

        public Task<BussinessModel<bool>> AddRole(UserModel userModel)
        {
            throw new System.NotImplementedException();
        }

       
    }
}
