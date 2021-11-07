using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.DataBase;
using XjjXmm.FrameWork.DependencyInjection;

namespace XjjXmm.Authorize.Repository
{
    [Injection]
    public class RoleRepository: Repository<RoleEntity>
    {
        public RoleRepository(DbClient dbclient) : base(dbclient)
        {
        }

        public async Task<IEnumerable<RoleEntity>> FindByUserId(long userId)
        {
            return await _dbclient.ComplexQueryable<RoleEntity>("r")
                 .Join<UserRoleEntity>("ur", (r, ur) => r.Id == ur.RoleId)
                 .Where((r, ur) => ur.UserId == userId)
                 .ExecuteQuery();

        }


    }
}
