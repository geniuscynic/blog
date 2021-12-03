using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.Role.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.Role.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.Role
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