
using Admin.Repository.Dictionary;
using Admin.Service.Dictionary;
using Admin.Service.Dictionary.Input;
using Admin.Service.Dictionary.Output;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;

namespace Admin.Core.Controllers.Admin
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        /// <summary>
        /// 查询单条数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DictionaryGetOutput> Get(long id)
        {
            return await _dictionaryService.Get(id);
        }

        /// <summary>
        /// 查询分页数据字典
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<DictionaryListOutput>> GetPage(PageInput<DictionaryEntity> model)
        {
            return await _dictionaryService.Page(model);
        }

        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(DictionaryAddInput input)
        {
            return await _dictionaryService.Add(input);
        }

        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(DictionaryUpdateInput input)
        {
            return await _dictionaryService.Update(input);
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _dictionaryService.SoftDelete(id);
        }

        /// <summary>
        /// 批量删除数据字典
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _dictionaryService.BatchSoftDelete(ids);
        }
    }
}