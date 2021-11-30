using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Repository.Criteria;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.Common;

namespace XjjXmm.Authorize.Api.Controllers
{
    /// <summary>
    /// 系统：字典详情管理
    /// </summary>
    [ApiController]
    [Route("api/dictDetail")]
    public class DictDetailController : ControllerBase
    {
        private readonly DictDetailService _dictDetailService;


        public DictDetailController(DictDetailService dictDetailService)
        {
            _dictDetailService = dictDetailService;
        }

        /// <summary>
        /// 查询字典详情
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<DictDetailDto>> Query([FromQuery] DictDetailQueryCriteria criteria)
        {
            return await _dictDetailService.QueryAll(criteria);
        }
    }
}
