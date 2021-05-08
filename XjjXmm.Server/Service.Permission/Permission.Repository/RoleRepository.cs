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
    }
}
