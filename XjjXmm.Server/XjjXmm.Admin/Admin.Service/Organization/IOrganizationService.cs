using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.FrameWork.LogExtension;

namespace Admin.Service.Organization
{
    [ProcessLog]
    public partial interface IOrganizationService
    {
        Task<OrganizationGetOutput> Get(long id);

        Task<IEnumerable<OrganizationListOutput>> GetList(OrganizationListInput key);

        Task<bool> Add(OrganizationAddInput input);

        Task<bool> Update(OrganizationUpdateInput input);

        Task<bool> Delete(long id);

        Task<bool> SoftDelete(long id);
    }
}