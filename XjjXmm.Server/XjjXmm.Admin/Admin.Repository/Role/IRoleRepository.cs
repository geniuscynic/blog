using XjjXmm.FrameWork.Common;

namespace Admin.Repository.Role;

public interface IRoleRepository : IRepositoryBase<RoleEntity>
{
    //Task<IEnumerable<RoleEntity>> GetByUserId(long userId); 
    Task<PageOutput<RoleEntity>> Page(PageInput<RoleEntity> input);
}
