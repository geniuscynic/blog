using System.Collections.Generic;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database;
using DoCare.Zkzx.Core.Database.Imp.Operate;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.FrameWork.DependencyInjection;

namespace XjjXmm.Authorize.Repository
{
    [Injection]
    public class UserRepository: Repository<UserEntity>
    {
        public UserRepository(Dbclient dbclient) : base(dbclient)
        {
        }

        //public Task<List<UserEntity>> GetUser(Expression<Func<UserEntity, bool>> whereExpression = null)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<(IEnumerable<UserEntity> users, int total)> GetUsers(SearchUserModel model)
        {
            var query = _dbclient.ComplexQueryable<UserEntity>("u");
               

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                query = query.Where((u) => SqlFunc.Like(u.Account, model.Name) || SqlFunc.Like(u.NickName, model.Name));
            }

            query = query.OrderByDesc((u) => u.CreateTime);

            return await query.ToPageList(model.PageIndex, model.PageSize);
        }
    }
}
