using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Admin.Service.Api;
using XjjXmm.FrameWork.Common;
using Admin.Repository.ApiEntity;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// 接口管理
    /// </summary>
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    public class ApiController : ControllerBase
    {
        private readonly IApiService _apiService;

        public ApiController(IApiService apiService)
        {
            _apiService = apiService;
        }

        /// <summary>
        /// 查询单条接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiGetOutput> Get(long id)
        {
            return await _apiService.GetAsync(id);
        }

        /// <summary>
        /// 查询全部接口
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ApiListOutput>> GetList(string key)
        {
            return await _apiService.ListAsync(key);
        }

        /// <summary>
        /// 查询分页接口
        /// </summary>
        /// <param name="model">分页模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<ApiEntity>> GetPage(PageInput<ApiEntity> model)
        {
            return await _apiService.PageAsync(model);
        }

        /// <summary>
        /// 新增接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(ApiAddInput input)
        {
            return await _apiService.AddAsync(input);
        }

        /// <summary>
        /// 修改接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(ApiUpdateInput input)
        {
            return await _apiService.UpdateAsync(input);
        }

        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _apiService.SoftDelete(id);
        }

        /// <summary>
        /// 批量删除接口
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _apiService.BatchSoftDelete(ids);
        }

        /// <summary>
        /// 同步接口
        /// 支持新增和修改接口
        /// 根据接口是否存在自动禁用和启用api
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Sync(ApiSyncInput input)
        {
            return await _apiService.SyncAsync(input);
        }
    }
}