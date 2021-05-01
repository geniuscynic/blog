using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database;
using Permission.Entity;

namespace Permission.IRepository
{
    public interface IRoleRepository : IRepository<Role>
    {
        //Task<List<User>> GetUser(Expression<Func<User, bool>> whereExpression = null);
    }
}
