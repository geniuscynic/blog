using Admin.Core.Model.Admin;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Repository.Admin
{
    [Injection]
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(MyUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}