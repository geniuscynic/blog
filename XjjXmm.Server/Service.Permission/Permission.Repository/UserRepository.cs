using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database;
using Permission.Entity;
using Permission.IRepository;


namespace Permission.Repository
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(Dbclient dbclient) : base(dbclient)
        {
        }

        public Task<List<User>> GetUser(Expression<Func<User, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
    }
}
