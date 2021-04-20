using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Permission.Entity;
using XjjXmm.Core.FrameWork.Repository;

namespace Permission.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUser(Expression<Func<User, bool>> whereExpression = null);
    }
}
