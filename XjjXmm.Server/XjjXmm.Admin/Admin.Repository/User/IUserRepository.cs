using Admin.Repository;
using XjjXmm.FrameWork.Common;

namespace Admin.Repository.User
{
    public interface IUserRepository : IRepositoryBase<UserEntity>
    {
        Task<PageOutput<UserEntity>> QueryPage(PageInput<UserEntity> input);
    }
}