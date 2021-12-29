using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;
using Admin.Service.Role.Input;
using Admin.Service.Role.Output;
using XjjXmm.FrameWork.Common;
using Admin.Repository.Role;

namespace Admin.Service.Role
{
    [ProcessLog]
    public interface IRoleService
    {
        Task<RoleGetOutput> Get(long id);

        Task<PageOutput<RoleListOutput>> Page(PageInput<RoleEntity> input);

        Task<bool> Add(RoleAddInput input);

        Task<bool> Update(RoleUpdateInput input);

        Task<bool> Delete(long id);

        Task<bool> SoftDelete(long id);

        Task<bool> BatchSoftDelete(long[] ids);
    }
}