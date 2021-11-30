using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository.Criteria;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.DataBase;
using XjjXmm.FrameWork.DependencyInjection;

namespace XjjXmm.Authorize.Repository
{
    [Injection]
    public class DictDetailRepository : Repository<DictDetailEntity>
    {

        public DictDetailRepository(DbClient dbclient) : base(dbclient)
        {
        }

        /**
       * 根据字典名称查询
       * @param name /
       * @return /
       */
        public async Task<IEnumerable<DictDetailEntity>> FindByDictName(string name)
        {
            return await _dbclient.ComplexQueryable<DictDetailEntity>("dd")
                .Join<DictEntity>("d", (dd, d) => dd.DictId == d.Id)
                .Where((dd, d) => d.Name == name)
                .ExecuteQuery();
        }

        public async Task<(IEnumerable<DictDetailEntity> data, int total)> FindAll(DictDetailQueryCriteria criteria)
        {
            return await _dbclient.ComplexQueryable<DictDetailEntity>("dd")
                .Include<DictEntity>(dd=>dd.Dict, dd=>dd.DictId, d=>d.Id)
                .Join<DictEntity>("d", (dd, d) => dd.DictId == d.Id)
                .Where((dd, d) => d.Name == criteria.DictName)
                .ToPageList(criteria.PageNumber, criteria.PageSize);
        }
    }
}
