using SqlSugar;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository.User
{
    [Injection]
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}