using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Admin;
using Admin.Core.Service.Admin.Tenant.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.Tenant.Output;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Core.Service.Admin.Tenant
{
    [ProcessLog]
    public interface ITenantService
    {
        Task<TenantGetOutput> GetAsync(long id);

        Task<PageOutput<TenantListOutput>> PageAsync(PageInput<TenantEntity> input);

        Task<bool> AddAsync(TenantAddInput input);

        Task<bool> UpdateAsync(TenantUpdateInput input);

        Task<bool> DeleteAsync(long id);

        Task<bool> SoftDeleteAsync(long id);

        Task<bool> BatchSoftDeleteAsync(long[] ids);
    }
}