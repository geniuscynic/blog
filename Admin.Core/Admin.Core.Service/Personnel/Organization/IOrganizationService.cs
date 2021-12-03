using System.Collections.Generic;
using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Personnel;
using Admin.Core.Service.Personnel.Organization.Input;
using System.Threading.Tasks;
using Admin.Core.Service.Personnel.Organization.Output;

namespace Admin.Core.Service.Personnel.Organization
{
    public partial interface IOrganizationService
    {
        Task<OrganizationGetOutput> GetAsync(long id);

        Task<List<OrganizationListOutput>> GetListAsync(string key);

        Task<bool> AddAsync(OrganizationAddInput input);

        Task<bool> UpdateAsync(OrganizationUpdateInput input);

        Task<bool> DeleteAsync(long id);

        Task<bool> SoftDeleteAsync(long id);
    }
}