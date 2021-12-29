
using Admin.Repository.Dictionary;
using Admin.Service.Dictionary.Input;
using Admin.Service.Dictionary.Output;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.Dictionary
{
    [Injection]
    public class DictionaryService : BaseService, IDictionaryService
    {
        private readonly IDictionaryRepository _dictionaryRepository;

        public DictionaryService(IDictionaryRepository dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }

        public async Task<DictionaryGetOutput> Get(long id)
        {
            var result = await _dictionaryRepository.GetById(id);
            var entity = result.MapTo< DictionaryEntity,DictionaryGetOutput>();
            return entity;
        }

        public async Task<PageOutput<DictionaryListOutput>> Page(PageInput<DictionaryEntity> input)
        {
            //var key = input.Filter?.Name;
            //var dictionaryTypeId = input.Filter?.DictionaryTypeId;
            //var list = await _dictionaryRepository.Select
            //.WhereIf(dictionaryTypeId.HasValue && dictionaryTypeId.Value > 0, a => a.DictionaryTypeId == dictionaryTypeId)
            //.WhereIf(key.NotNull(), a => a.Name.Contains(key) || a.Code.Contains(key))
            //.Count(out var total)
            //.OrderByDescending(true, c => c.Id)
            //.Page(input.CurrentPage, input.PageSize)
            //.ToListAsync<DictionaryListOutput>();

            //var data = new PageOutput<DictionaryListOutput>()
            //{
            //    List = list,
            //    Total = total
            //};

            //return ResponseOutput.Ok(data);

            return null;
        }

        public async Task<bool> Add(DictionaryAddInput input)
        {
            var dictionary = input.MapTo<DictionaryAddInput, DictionaryEntity>();
            var id = await _dictionaryRepository.Add(dictionary);
            return id > 0;
        }

        public async Task<bool> Update(DictionaryUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            //var entity = await _dictionaryRepository.GetAsync(input.Id);
            //if (!(entity?.Id > 0))
            //{
            //    return ResponseOutput.NotOk("数据字典不存在！");
            //}

            var dictionary = input.MapTo<DictionaryUpdateInput, DictionaryEntity>();

           return await _dictionaryRepository.Update(dictionary);
           
        }

        public async Task<bool> Delete(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = await _dictionaryRepository.Delete(id);
            }

            return result;
        }

        public async Task<bool> SoftDelete(long id)
        {
            return await _dictionaryRepository.SoftDelete(id);
        }

        public async Task<bool> BatchSoftDelete(long[] ids)
        {
           return await _dictionaryRepository.SoftDelete(ids);

           
        }
    }
}