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
    public class ApiMethodRepository : Repository<ApiMethod> , IApiMethodRepository
    {
        public ApiMethodRepository(Dbcontext dbcontext) : base(dbcontext)
        {
        }

        public async Task<bool> HasApiMethodPermission(List<string> roleCode, string route, string httpMethod)
        {
            return  await Db.Queryable<ApiMethod, ApiMethodPermission, Role>(
                    (t1, t2, t3) => t1.Id == t2.ApiId && t2.RoleId == t3.Id)
                .Where((t1, t2, t3) => roleCode.Contains(t3.Code))
                .Where(t1 => t1.RoutePath == route && t1.HttpMethod == httpMethod)
                .Select(t1 => t1)
                .AnyAsync();
        }
    }
}
