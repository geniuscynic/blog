
using Admin.Repository.DictionaryType;
using Admin.Service.DictionaryType;
using Admin.Service.DictionaryType.Input;
using Admin.Service.DictionaryType.Output;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;

namespace Admin.Core.Controllers.Admin
{
    /// <summary>
    /// 数据字典类型
    /// </summary>
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    public class DictionaryTypeController : ControllerBase
    {
        private readonly IDictionaryTypeService _DictionaryTypeService;

        public DictionaryTypeController(IDictionaryTypeService DictionaryTypeService)
        {
            _DictionaryTypeService = DictionaryTypeService;
        }

        /// <summary>
        /// 查询单条数据字典类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DictionaryTypeGetOutput> Get(long id)
        {
            return await _DictionaryTypeService.Get(id);
        }

        /// <summary>
        /// 查询分页数据字典类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<DictionaryTypeListOutput>> GetPage(PageInput<DictionaryTypeEntity> model)
        {
            return await _DictionaryTypeService.Page(model);
        }

        /// <summary>
        /// 新增数据字典类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(DictionaryTypeAddInput input)
        {
            return await _DictionaryTypeService.Add(input);
        }

        /// <summary>
        /// 修改数据字典类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(DictionaryTypeUpdateInput input)
        {
            return await _DictionaryTypeService.Update(input);
        }

        /// <summary>
        /// 删除数据字典类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _DictionaryTypeService.SoftDelete(id);
        }

        /// <summary>
        /// 批量删除数据字典类型
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _DictionaryTypeService.BatchSoftDelete(ids);
        }
    }
}