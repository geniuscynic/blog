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
    public class DeptRepository : Repository<DeptEntity>
    {
        public DeptRepository(DbClient dbclient) : base(dbclient)
        {
        }

        public async Task<IEnumerable<DeptEntity>> FindByRoleId(long id)
        {
            return await _dbclient.ComplexQueryable<DeptEntity>("d")
                .Join<RoleDeptEntity>("rd", (d, rd) => d.Id == rd.DeptId)
                .Where((d, rd) => rd.RoleId == id)
                .ExecuteQuery();
        }
    }
}
