using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.IRepository;
using SqlSugar;

namespace Blog.Repository
{
    public class UserRepository: Repository<User> , IUserRepository
    {
        public UserRepository(Dbcontext dbcontext) : base(dbcontext)
        {
        }

        public async Task<List<User>> GetUser(Expression<Func<User, bool>> whereExpression = null)
        {
            var user = Db.Queryable<User>()
                .Mapper((result, cache) =>
                {
                    var cres = cache.Get(l =>
                    {
                        var r1 = Db.Queryable<UserRole>()
                            .Mapper(ur => ur.Role, ur => ur.RoleId)
                            .In(it => it.UserId, l.Select(it => it.Id).ToArray()).ToList();
                        //.ToListAsync().Result;

                        return r1;
                    });


                    result.Roles = cres.Where(it => it.UserId == result.Id)
                        .Select(it => it.Role).ToList();
                });

            if (whereExpression != null)
            {
                return await user.Where(whereExpression).ToListAsync();
            }

            return await user.ToListAsync();
        }
    }
}
