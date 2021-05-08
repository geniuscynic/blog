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
    public class UserRepository: Repository<UserEntity>, IUserRepository
    {
        public UserRepository(Dbclient dbclient) : base(dbclient)
        {
        }

        //public Task<List<UserEntity>> GetUser(Expression<Func<UserEntity, bool>> whereExpression = null)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<(IEnumerable<UserEntity> users, int total)> GetUsers(string name, int pageIndex, int pageSize)
        {
            var query = _dbclient.ComplexQueryable<UserEntity>("u");
               

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where((u) => SqlFunc.Like(u.Account, name) || SqlFunc.Like(u.NickName, name));
            }

            query = query.OrderByDesc((u) => u.CreateTime);

            return await query.ToPageList(pageIndex, pageSize);
        }
    }
}
