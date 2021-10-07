using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entity;

namespace Blog.IRepository
{
    public interface IApiMethodRepository : IRepository<ApiMethod>
    {
        Task<bool> HasApiMethodPermission(List<string> roleCode, string route, string httpMethod);
    }
}
