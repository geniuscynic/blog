using Admin.Repository.Organization;
using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.Organization
{
    [Injection]
    public class OrganizationService : BaseService, IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<OrganizationGetOutput> Get(long id)
        {
            var result = await _organizationRepository.GetById(id);
            return result.MapTo<OrganizationEntity, OrganizationGetOutput>();
        }

        public async Task<IEnumerable<OrganizationListOutput>> GetList(OrganizationListInput input)
        {
            var dto = await _organizationRepository.GetList(input.Key);
             return dto.MapTo<OrganizationEntity, OrganizationListOutput>();
        }
                                                                                                                  
        public async Task<bool> Add(OrganizationAddInput input)
        {
            var dictionary = input.MapTo<OrganizationAddInput, OrganizationEntity>();
            Fill(dictionary, FillStatus.Add);

         
            return await _organizationRepository.Add(dictionary) > 0;
        }

        public async Task<bool> Update(OrganizationUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            //var entity = await _organizationRepository.GetAsync(input.Id);
            //if (!(entity?.Id > 0))
            //{
            //    return ResponseOutput.NotOk("数据字典不存在！");
            //}

            var entity = input.MapTo<OrganizationUpdateInput, OrganizationEntity>();
            
            return await _organizationRepository.Update(entity);
            
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(long id)
        {
            //todo

            //var result = await _organizationRepository.DeleteRecursiveAsync(a => a.Id == id);

            //return ResponseOutput.Result(result);
            return false;
        }

        public async Task<bool> SoftDelete(long id)
        {
            //var result = await _organizationRepository.SoftDeleteRecursiveAsync(a => a.Id == id);

            //return ResponseOutput.Result(result);

            return false;
        }
    }
}