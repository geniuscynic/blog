using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Repository
{
    public interface IBaseRepository<T> //: ISimpleClient<T> where T : class, new()
    {
        Task<T> GetFirst(Expression<Func<T, bool>> whereExpression);
    }
}
