﻿using Admin.Repository.Dictionary;
using Admin.Repository.DictionaryType;

using Admin.Service.DictionaryType.Output;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;

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

        public async Task<DictionaryTypeGetOutput> GetAsync(long id)
        {
            var result = await _DictionaryTypeRepository.GetAsync<DictionaryTypeGetOutput>(id);
            return result;
        }

        public async Task<PageOutput<DictionaryTypeListOutput>> PageAsync(PageInput<DictionaryTypeEntity> input)
        {
            var key = input.Filter?.Name;

            var list = await _DictionaryTypeRepository.Select
            .WhereIf(key.NotNull(), a => a.Name.Contains(key) || a.Code.Contains(key))
            .Count(out var total)
            .OrderByDescending(true, c => c.Id)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync<DictionaryTypeListOutput>();

            var data = new PageOutput<DictionaryTypeListOutput>()
            {
                List = list,
                Total = total
            };

            return data;
        }

        public async Task<bool> AddAsync(DictionaryTypeAddInput input)
        {
            var DictionaryType = Mapper.Map<DictionaryTypeEntity>(input);
            var id = (await _DictionaryTypeRepository.InsertAsync(DictionaryType)).Id;
            return id > 0;
        }

        public async Task<bool> UpdateAsync(DictionaryTypeUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return false;
            }

            var entity = await _DictionaryTypeRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                //return ResponseOutput.NotOk("数据字典不存在！");
                throw new BussinessException(StatusCodes.Status999Falid, "数据字典不存在！");
            }

            Mapper.Map(input, entity);
            await _DictionaryTypeRepository.UpdateAsync(entity);
            //return ResponseOutput.Ok();
            return true;
        }

        [Transaction]
        public async Task<bool> DeleteAsync(long id)
        {
            //删除字典数据
            await _dictionaryRepository.DeleteAsync(a => a.DictionaryTypeId == id);

            //删除字典类型
            await _DictionaryTypeRepository.DeleteAsync(a => a.Id == id);

            return true;
        }

        [Transaction]
        public async Task<bool> SoftDeleteAsync(long id)
        {
            await _dictionaryRepository.SoftDeleteAsync(a => a.DictionaryTypeId == id);
            await _DictionaryTypeRepository.SoftDeleteAsync(id);

            return true;
        }

        [Transaction]
        public async Task<bool> BatchSoftDeleteAsync(long[] ids)
        {
            await _dictionaryRepository.SoftDeleteAsync(a => ids.Contains(a.DictionaryTypeId));
            await _DictionaryTypeRepository.SoftDeleteAsync(ids);

            return true;
        }
    }
}