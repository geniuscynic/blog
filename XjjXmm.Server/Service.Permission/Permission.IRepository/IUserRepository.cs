﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.Database;
using Permission.Entity;

namespace Permission.IRepository
{
    public interface IUserRepository : IRepository<UserEntity>
    {
       // Task<List<UserEntity>> GetUser(Expression<Func<UserEntity, bool>> whereExpression = null);

        Task<(IEnumerable<UserEntity> users, int total)> GetUsers(string name, int pageIndex, int pageSize);
    }
}
