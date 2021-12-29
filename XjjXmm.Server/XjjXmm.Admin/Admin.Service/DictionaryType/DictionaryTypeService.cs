using Admin.Repository.Dictionary;
using Admin.Repository.DictionaryType;
using Admin.Service.DictionaryType.Input;
using Admin.Service.DictionaryType.Output;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace Admin.Service.DictionaryType
{
    [Injection]
    public class DictionaryTypeService : BaseService, IDictionaryTypeService
    {
        private readonly IDictionaryTypeRepository _DictionaryTypeRepository;
        private readonly IDictionaryRepository _dictionaryRepository;
        public DictionaryTypeService(IDictionaryTypeRepository DictionaryTypeRepository, IDictionaryRepository dictionaryRepository)
        {
            _DictionaryTypeRepository = DictionaryTypeRepository;
            _dictionaryRepository = dictionaryRepository;
        }

        public async Task<DictionaryTypeGetOutput> Get(long id)
        {
            var result = await _DictionaryTypeRepository.GetById(id);
            var dto = result.MapTo<DictionaryTypeEntity, DictionaryTypeGetOutput>();
            return dto;
        }

        public async Task<PageOutput<DictionaryTypeListOutput>> Page(PageInput<DictionaryTypeEntity> input)
        {
            //var key = input.Filter?.Name;

            //var list = await _DictionaryTypeRepository.Select
            //.WhereIf(key.NotNull(), a => a.Name.Contains(key) || a.Code.Contains(key))
            //.Count(out var total)
            //.OrderByDescending(true, c => c.Id)
            //.Page(input.CurrentPage, input.PageSize)
            //.ToListAsync<DictionaryTypeListOutput>();

            //var data = new PageOutput<DictionaryTypeListOutput>()
            //{
            //    List = list,
            //    Total = total
            //};

            //return data;

            return null;
        }

        public async Task<bool> Add(DictionaryTypeAddInput input)
        {
            var DictionaryType = input.MapTo<DictionaryTypeAddInput, DictionaryTypeEntity>();
            var id = await _DictionaryTypeRepository.Add(DictionaryType);
            return id > 0;
        }

        public async Task<bool> Update(DictionaryTypeUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            //var entity = await _DictionaryTypeRepository.GetAsync(input.Id);
            //if (!(entity?.Id > 0))
            //{
            //    //return ResponseOutput.NotOk("数据字典不存在！");
            //    throw new BussinessException(StatusCodes.Status999Falid, "数据字典不存在！");
            //}

            var entity = input.MapTo<DictionaryTypeAddInput, DictionaryTypeEntity>();
            return await _DictionaryTypeRepository.Update(entity);
            //return ResponseOutput.Ok();
           
        }

        //[Transaction]
        public async Task<bool> Delete(long id)
        {
            //删除字典数据
            await _dictionaryRepository.Delete(a => a.DictionaryTypeId == id);

            //删除字典类型
            await _DictionaryTypeRepository.Delete(a => a.Id == id);

            return true;
        }

        //[Transaction]
        public async Task<bool> SoftDelete(long id)
        {
            await _dictionaryRepository.SoftDelete(a => a.DictionaryTypeId == id);
            await _DictionaryTypeRepository.SoftDelete(id);

            return true;
        }

        //[Transaction]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            await _dictionaryRepository.SoftDelete(a => ids.Contains(a.DictionaryTypeId));
            await _DictionaryTypeRepository.SoftDelete(ids);

            return true;
        }
    }
}