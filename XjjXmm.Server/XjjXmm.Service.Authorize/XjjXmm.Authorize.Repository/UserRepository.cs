using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XjjXmm.Authorize.Repository.Entity;
using XjjXmm.DataBase;
using XjjXmm.DataBase.Imp.Operate;
using XjjXmm.FrameWork.DependencyInjection;

namespace XjjXmm.Authorize.Repository
{
    [Injection]
    public class UserRepository: Repository<UserEntity>
    {
        public UserRepository(DbClient dbclient) : base(dbclient)
        {
        }

        //public Task<List<UserEntity>> GetUser(Expression<Func<UserEntity, bool>> whereExpression = null)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<(IEnumerable<UserEntity> users, int total)> GetUsers(SearchUserModel model)
        {
            //var query = _dbclient.ComplexQueryable<UserEntity>("u");


            //if (!string.IsNullOrWhiteSpace(model.Name))
            //{
            //    query = query.Where((u) => SqlFunc.Like(u.Account, model.Name) || SqlFunc.Like(u.NickName, model.Name));
            //}

            //query = query.OrderByDesc((u) => u.CreateTime);

            //return await query.ToPageList(model.PageIndex, model.PageSize);
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserEntity>> FindByLoginName(string loginName)
        {

            var dictionary = new Dictionary<long, UserEntity>();

            return await _dbclient.ComplexQueryable<UserEntity>("u")
                .Join<UserRoleEntity>("ur", (u, ur) => u.Id == ur.UserId)
                .Join<RoleEntity>("r", (u, ur, r) => ur.RoleId == r.Id)
                .Where((u)=>u.UserName == loginName)
                .ExecuteQuery<RoleEntity>((entity, roleEntity) =>
                {
                    if (!dictionary.TryGetValue(entity.Id, out var userEntity))
                    {
                        userEntity = entity;
                        userEntity.Roles = new List<RoleEntity>();
                        dictionary.Add(userEntity.Id, userEntity);
                    }

                    userEntity.Roles.Add(roleEntity);
                    return userEntity;
                }, "roleId");
        }
    }
}
