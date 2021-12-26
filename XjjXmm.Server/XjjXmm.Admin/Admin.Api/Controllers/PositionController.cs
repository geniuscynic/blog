using Admin.Repository.Position;
using Admin.Service.Position;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// 职位管理
    /// </summary>
    [ApiController]
    [Route("api/personnel/[controller]/[action]")]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        /// <summary>
        /// 查询单条职位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PositionGetOutput> Get(long id)
        {
            return await _positionService.Get(id);
        }

        /// <summary>
        /// 查询分页职位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<PositionListOutput>> GetPage(PageInput<PositionListInput> model)
        {
            return await _positionService.Page(model);
        }

        /// <summary>
        /// 新增职位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(PositionAddInput input)
        {
            return await _positionService.Add(input);
        }

        /// <summary>
        /// 修改职位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Update(PositionUpdateInput input)
        {
            return await _positionService.Update(input);
        }

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _positionService.SoftDelete(id);
        }

        /// <summary>
        /// 批量删除职位
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDelete(long[] ids)
        {
            return await _positionService.BatchSoftDelete(ids);
        }
    }
}