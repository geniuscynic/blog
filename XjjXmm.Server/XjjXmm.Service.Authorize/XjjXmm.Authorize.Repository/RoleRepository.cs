using System.Collections.Generic;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.FrameWork.DependencyInjection;

namespace XjjXmm.Authorize.Repository
{
    [Injection]
    public class RoleRepository: Repository<RoleEntity>
    {
        public RoleRepository(Dbclient dbclient) : base(dbclient)
        {
        }

        public async Task<IEnumerable<RoleEntity>> GetRoleByUserId(string userId)
        {
           return await _dbclient.ComplexQueryable<RoleEntity>("r")
                .Join<UserRoleEntity>("ur", (r, ur) => r.Id == ur.RoleId)
                .Where((r, ur) => ur.Id == userId)
                .ExecuteQuery();
        }
    }
}
