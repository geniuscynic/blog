using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.DataBase;
using XjjXmm.FrameWork.DependencyInjection;

namespace XjjXmm.Authorize.Repository
{
    [Injection]
    public class DictRepository : Repository<DictEntity>
    {
        /**
    * 删除
    * @param ids /
    */
       // void deleteByIdIn(Set<Long> ids);

        /**
         * 查询
         * @param ids /
         * @return /
         */
       // List<Dict> findByIdIn(Set<Long> ids);
       public DictRepository(DbClient dbclient) : base(dbclient)
       {
       }
    }
}
