using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Models;
using Blog.IRepository;
using Dapper;

namespace Blog.Repository
{
    public class UserRepository  : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<User> Add(User model)
        {
            var sql = "INSERT INTO [dbo].[User] ([Account],[Password],[NickName],[LoginTime]) VALUES(@Account,@Password,@NickName,@LoginTime); " +
                      "select @@IDENTITY";

            var id = await _connection.ExecuteScalarAsync<int>(sql, model);

            model.Id = id;

            return model;
        }

        public Task<bool> Add(List<User> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(User model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById<T>(T id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIds<T>(T[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> Query(Expression<Func<User, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<User> QueryById(object id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            var sql = "select * from [User]";

            var user = await _connection.QueryAsync<User>(sql);
               
            return user;
            //var addUserModel = mapper.Map<List<User>, List<AddUserViewModel>>(user);
            //addUserModel.Ro

        }
    }
}
