using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;
using Admin.Service.Role.Input;
using Admin.Service.Role.Output;
using XjjXmm.FrameWork.Common;

namespace Admin.Service.Role
{
    [ProcessLog]
    public interface IRoleService
    {
        Task<RoleGetOutput> GetAsync(long id);

        Task<PageOutput<RoleListOutput>> PageAsync(PageInput<RoleEntity> input);

        Task<bool> AddAsync(RoleAddInput input);

        Task<bool> UpdateAsync(RoleUpdateInput input);

        Task<bool> DeleteAsync(long id);

        Task<bool> SoftDeleteAsync(long id);

        Task<bool> BatchSoftDeleteAsync(long[] ids);
    }
}