using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database;
using DoCare.Zkzx.Core.Database.Imp.Operate;
using Permission.Entity;
using Permission.IRepository;


namespace Permission.Repository
{
    public class RoleRepository: Repository<RoleEntity>, IRoleRepository
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
