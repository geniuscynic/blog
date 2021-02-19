using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Dao.Operate
{
    public interface IXjjXmmQueryable<T>  : IQueryOperate<T>
    {
        IXjjXmmQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IXjjXmmQueryable<T> Where(string whereExpression);

        IXjjXmmQueryable<T> Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate);

        IQueryOperate<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate);
    }
}
