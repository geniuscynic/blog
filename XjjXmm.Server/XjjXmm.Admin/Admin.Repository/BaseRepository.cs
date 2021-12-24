using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Repository
{
          
    public class BaseRepository<T> : IBaseRepository<T> //where T : class, new()
    {
        private readonly ISqlSugarClient context;
                                               // SimpleClient
        public BaseRepository(ISqlSugarClient context) 
        {
            this.context = context;
        }

        public async Task<T> GetFirst(Expression<Func<T, bool>> whereExpression)
        {
            return await context.Queryable<T>().Where(whereExpression).FirstAsync();
        }
    }
}
