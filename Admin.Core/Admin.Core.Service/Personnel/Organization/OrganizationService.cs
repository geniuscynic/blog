using System.Collections.Generic;
using Admin.Core.Common.Input;
using Admin.Core.Common.Output;
using Admin.Core.Model.Personnel;
using Admin.Core.Repository.Personnel;
using Admin.Core.Service.Personnel.Organization.Input;
using Admin.Core.Service.Personnel.Organization.Output;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Core.Service.Personnel.Organization
{
    [Injection]
    public class OrganizationService : BaseService, IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<OrganizationGetOutput> GetAsync(long id)
        {
            var result = await _organizationRepository.GetAsync<OrganizationGetOutput>(id);
            return result;
        }

        public async Task<List<OrganizationListOutput>> GetListAsync(string key)
        {
            var data = await _organizationRepository
                .WhereIf(key.NotNull(), a => a.Name.Contains(key) || a.Code.Contains(key))
                .OrderBy(a => a.ParentId)
                .OrderBy(a => a.Sort)
                .ToListAsync<OrganizationListOutput>();

            return data;
        }

        public async Task<bool> AddAsync(OrganizationAddInput input)
        {
            var dictionary = Mapper.Map<OrganizationEntity>(input);
            var id = (await _organizationRepository.InsertAsync(dictionary)).Id;
            return id > 0;
        }

        public async Task<bool> UpdateAsync(OrganizationUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            var entity = await _organizationRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                //return ResponseOutput.NotOk("数据字典不存在！");
                throw new BussinessException(StatusCodes.Status999Falid, "数据字典不存在！");
            }

            Mapper.Map(input, entity);
            await _organizationRepository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = await _organizationRepository.DeleteRecursiveAsync(a => a.Id == id);

            return result;
        }

        public async Task<bool> SoftDeleteAsync(long id)
        {
            var result = await _organizationRepository.SoftDeleteRecursiveAsync(a => a.Id == id);

            return result;
        }
    }
}