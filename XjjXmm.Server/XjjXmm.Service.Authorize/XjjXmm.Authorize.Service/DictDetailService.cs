using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository;
using XjjXmm.Authorize.Repository.Criteria;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.Authorize.Service.Model;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DependencyInjection;
using XjjXmm.FrameWork.Mapper;

namespace XjjXmm.Authorize.Service
{
    [Injection]
    public class DictDetailService
    {
        private readonly DictDetailRepository _dictDetailRepository;

        public DictDetailService(DictDetailRepository dictDetailRepository)
        {
            _dictDetailRepository = dictDetailRepository;
        }

        public virtual async  Task<PageModel<DictDetailDto>> QueryAll(DictDetailQueryCriteria criteria)
        {
            var model = await _dictDetailRepository.FindAll(criteria);
            var data = model.data.MapTo<DictDetailEntity, DictDetailDto>();

            return new PageModel<DictDetailDto>()
            {
                Data = data,
                Total = model.total,
                Page =  criteria.PageNumber,
                PageSize = criteria.PageSize
            };
        }
    }
}
