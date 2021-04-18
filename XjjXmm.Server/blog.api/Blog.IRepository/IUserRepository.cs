using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entity;

namespace Blog.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUser(Expression<Func<User, bool>> whereExpression = null);
    }
}
